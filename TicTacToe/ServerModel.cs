﻿using System;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace TicTacToe
{
    class ServerModel : IServerModel
    {
        private HttpListener mListener;
        private Game mGame = new Game();
        private ConcurrentDictionary<IPAddress, string> mPlayers = 
            new ConcurrentDictionary<IPAddress, string>();
        private Assembly mAssembly = Assembly.GetExecutingAssembly();

        public event Action<string, string, string> EventOccurred;

        public ServerModel()
        {
            mListener = new HttpListener();
            mListener.Prefixes.Add( "http://*:8181/" );
            mListener.Start();
            mListener.BeginGetContext( new AsyncCallback( ListenerCallback ), mListener );
        }

        public void Abort()
        {
            mListener.Abort();
            return;
        }

        public void ListenerCallback( IAsyncResult aResult )
        {
            // Complete this asynchronous operation.
            var lListener = ( HttpListener )aResult.AsyncState;
            var lContext = lListener.EndGetContext( aResult );

            // Start the next read.
            lListener.BeginGetContext( new AsyncCallback( ListenerCallback ), lListener );

            // Read the request
            var lRequest = lContext.Request;
            var lReader = new StreamReader( lRequest.InputStream );
            var lRequestContent = lReader.ReadToEnd();
            lReader.Close();
            lReader.Dispose();

            // Get the request file
            var lRequestFile = lRequest.RawUrl.Substring( 1 ).Split( new char[] { '?' } )[0];
            if ( lRequestFile == "" )
            {
                lRequestFile = "index.html";
            }

            // Construct the response
            var lResponse = lContext.Response;
            var lResponseString = "";
            var lStream = mAssembly.GetManifestResourceStream( "TicTacToe.Resources." + 
                lRequestFile.Replace( "/", "." ) );
            if ( lStream != null )
            {
                byte[] lBuffer = new byte[lStream.Length];
                lStream.Read( lBuffer, 0, lBuffer.Length );
                lResponse.OutputStream.Write( lBuffer, 0, lBuffer.Length );
            }
            else if ( lRequestFile == "SignIn" )
            {
                var lState = new JObject(
                    new JProperty( "success", false )
                );
                var lUserName = lRequest.QueryString["username"];

                lock ( mPlayers )
                {
                    if ( String.IsNullOrWhiteSpace( lUserName ) )
                    {
                        lState["message"] = "Username must contain letters/numbers.";
                    }
                    else if ( mPlayers.Values.Contains( lUserName ) )
                    {
                        lState["message"] = "Username has already been selected by someone else.";
                    }
                    else if ( mPlayers.Count < 2 )
                    {
                        mPlayers.AddOrUpdate( lRequest.RemoteEndPoint.Address, lUserName,
                            ( IPAddress aOldKey, string aOldValue ) => { return lUserName; } );
                        mGame.AddPlayer( lUserName );
                        lState["success"] = true;
                        lState["name"] = lUserName;
                    }
                }

                var lTextWriter = new StreamWriter( lResponse.OutputStream );
                lTextWriter.Write( lState.ToString() );
                lTextWriter.Dispose();
            }
            else if ( lRequestFile == "GameState" )
            {
                var lGameState = new JObject(
                    new JProperty( "player1", "Dan" ),
                    new JProperty( "player2", "Cate" ),
                    new JProperty( "activePlayer", "Cate" ),
                    new JProperty( "status", mGame.GetStatus() ),
                    new JProperty( "boxes",
                        new JArray( mGame.State.Boxes.Select( b => b == Box.Blank ? "" : b.ToString() ) ) )
                    );
                using ( var lTextWriter = new StreamWriter( lResponse.OutputStream ) )
                {
                    lTextWriter.Write( lGameState.ToString() );
                }
            }
            else if ( lRequestFile == "Move" )
            {
                var lState = new JObject(
                    new JProperty( "success", false )
                );
                var lBox = int.Parse( lRequest.QueryString["box"] ) - 1;
                if ( mPlayers.ContainsKey( lRequest.RemoteEndPoint.Address ) )
                {
                    if ( mGame.Move( lBox, mPlayers[lRequest.RemoteEndPoint.Address] ) == MoveResult.Success )
                    {
                        lState["success"] = true;
                    }
                }

                if ( mGame.State.Phase == Phase.OWon ||
                     mGame.State.Phase == Phase.Tie ||
                     mGame.State.Phase == Phase.XWon )
                {
                    Task.Factory.StartNew( () =>
                    {
                        Thread.Sleep( 5000 );
                        mGame.Reset();
                    } );
                }

                var lTextWriter = new StreamWriter( lResponse.OutputStream );
                lTextWriter.Write( lState.ToString() );
                lTextWriter.Dispose();
            }
            else
            {
                lResponse.StatusCode = 404;
            }

            // Write the response.
            var lWriter = new StreamWriter( lResponse.OutputStream );
            lWriter.Write( lResponseString );
            lWriter.Close();
            lWriter.Dispose();

            // Log the event.
            EventOccurred( lRequest.RemoteEndPoint.Address.ToString(), lRequest.RawUrl, 
                lResponse.StatusCode.ToString() + " " + lResponseString );
            return;
        }
    }
}
