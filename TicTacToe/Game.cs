using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TicTacToe
{
    class Game
    {
        public class GameState
        {
            public Box[] Boxes { get; set; }
            public Phase Phase { get; set; }
        }

        private object mLock = new object();
        private Random mRandom = new Random();
        private Box[] mBoxes = new Box[9];
        private Phase mPhase;
        private string mXPlayer;
        private string mOPlayer;

        public GameState State
        {
            get
            {
                GameState lResult;
                lock ( mLock )
                {
                    lResult = new GameState()
                    {
                        Boxes = mBoxes,
                        Phase = mPhase,
                    };
                }
                return lResult;
            }
        }

        public Game()
        {
            Reset();
        }

        public void Reset()
        {
            lock ( mLock )
            {
                for ( int lIndex = 0; lIndex < 9; lIndex++ )
                {
                    mBoxes[lIndex] = Box.Blank;
                }

                if ( mRandom.NextDouble() > 0.5 )
                {
                    mPhase = Phase.OTurn;
                }
                else
                {
                    mPhase = Phase.XTurn;
                }
            }
        }

        public void AddPlayer( string aPlayer )
        {
            if ( mXPlayer == null )
            {
                mXPlayer = aPlayer;
            }
            else if ( mOPlayer == null )
            {
                mOPlayer = aPlayer;
            }
        }

        public MoveResult Move( int aIndex, string aUsername )
        {
            lock ( mLock )
            {
                Box lBox = Box.Blank;
                if ( mXPlayer == aUsername )
                {
                    lBox = Box.X;
                }
                else if ( mOPlayer == aUsername )
                {
                    lBox = Box.O;
                }
                if ( aIndex >= 0 && aIndex <= 8 && mBoxes[aIndex] == Box.Blank &&
                     ( ( mPhase == Phase.OTurn && lBox == Box.O ) ||
                       ( mPhase == Phase.XTurn && lBox == Box.X ) ) )
                {
                    mBoxes[aIndex] = lBox;

                    // See if anyone won.
                    if ( HasWon( Box.O ) )
                    {
                        mPhase = Phase.OWon;
                    }
                    else if ( HasWon( Box.X ) )
                    {
                        mPhase = Phase.XWon;
                    }
                    else if ( mBoxes.Contains( Box.Blank ) == false )
                    {
                        mPhase = Phase.Tie;
                    }
                    else if ( mPhase == Phase.OTurn )
                    {
                        mPhase = Phase.XTurn;
                    }
                    else
                    {
                        mPhase = Phase.OTurn;
                    }

                    return MoveResult.Success;
                }
                else
                {
                    return MoveResult.Invalid;
                }
            }
        }

        public string GetStatus()
        {
            string lStatus = null;
            switch ( mPhase )
            {
                case Phase.XTurn:
                    if ( mXPlayer == null )
                    {
                        lStatus = "Waiting for X player to connect.";
                    }
                    else
                    {
                        lStatus = mXPlayer + "'s (X) Turn.";
                    }
                    break;
                case Phase.OTurn:
                    if ( mOPlayer == null )
                    {
                        lStatus = "Waiting for O player to connect.";
                    }
                    else
                    {
                        lStatus = mOPlayer + "'s (O) Turn.";
                    }
                    break;
                case Phase.XWon:
                    lStatus = mXPlayer + " (X) Won!";
                    break;
                case Phase.OWon:
                    lStatus = mOPlayer + " (O) Won!";
                    break;
                case Phase.Tie:
                    lStatus = "Everybody Loses!";
                    break;
                default:
                    break;
            }
            return lStatus;
        }

        private bool HasWon( Box aBox )
        {
            // check the rows in the game grid       
            for ( int lRowStartIndex = 0; lRowStartIndex < 9; lRowStartIndex += 3 )
            {
                if ( mBoxes[lRowStartIndex] == aBox &&
                     mBoxes[lRowStartIndex + 1] == aBox &&
                     mBoxes[lRowStartIndex + 2] == aBox )
                {
                    return true;
                }
            }

            // check the columns in the game grid       
            for ( int lColumnStartIndex = 0; lColumnStartIndex < 3; lColumnStartIndex += 1 )
            {
                if ( mBoxes[lColumnStartIndex] == aBox &&
                     mBoxes[lColumnStartIndex + 3] == aBox &&
                     mBoxes[lColumnStartIndex + 6] == aBox )
                {
                    return true;
                }
            }

            // check the diagonal line ( "\" ) in the game grid             
            if ( mBoxes[0] == aBox &&
                 mBoxes[4] == aBox &&
                 mBoxes[8] == aBox )
            {
                return true;
            }

            // check the diagonal line ( "/" ) in the game grid            
            if ( mBoxes[2] == aBox &&
                 mBoxes[4] == aBox &&
                 mBoxes[6] == aBox )
            {
                return true;
            }

            // If all the checks above fail, return false     
            return false;
        }
    }
}
