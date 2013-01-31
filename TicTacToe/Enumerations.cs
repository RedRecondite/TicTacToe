using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TicTacToe
{
    enum Box
    {
        Blank,
        X,
        O
    }

    enum Phase
    {
        XTurn,
        OTurn,
        XWon,
        OWon,
        Tie
    }

    enum MoveResult
    {
        Invalid,
        Success,
    }
}
