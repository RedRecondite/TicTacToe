using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace TicTacToe
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault( false );

            var lModel = new ServerModel();
            var lView = new ServerForm();
            Present( lModel, lView );

            Application.Run( lView );
        }

        private static void Present( IServerModel aModel, IServerView aView )
        {
            aModel.EventOccurred += aView.LogEvent;
            aView.AbortRequested += aModel.Abort;
            return;
        }
    }
}
