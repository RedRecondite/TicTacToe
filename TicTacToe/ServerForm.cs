using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TicTacToe
{
    public partial class ServerForm : Form, IServerView
    {
        public ServerForm()
        {
            InitializeComponent();
        }

        public event Action AbortRequested;

        public void LogEvent( string aSource, string aRequest, string aResponse )
        {
            this.InvokeIfRequired( () =>
            {
                var lItem = new ListViewItem( new string[] { aSource, aRequest, aResponse } );
                logListView.Items.Add( lItem );
            } );
        }

        private void ServerForm_FormClosing( object sender, FormClosingEventArgs e )
        {
            AbortRequested();
        }
    }
}
