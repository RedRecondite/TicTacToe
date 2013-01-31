using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TicTacToe
{
    interface IServerModel
    {
        event Action<string, string, string> EventOccurred;
        void Abort();
    }
}
