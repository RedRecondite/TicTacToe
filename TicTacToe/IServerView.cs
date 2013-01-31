using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TicTacToe
{
    interface IServerView
    {
        event Action AbortRequested;
        void LogEvent( string aSource, string aRequest, string aResponse );
    }
}
