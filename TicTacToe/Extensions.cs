using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TicTacToe
{
    static class Extensions
    {
        static public void InvokeIfRequired( this Form aForm, Action aAction )
        {
            if ( aForm.InvokeRequired )
            {
                aForm.Invoke( aAction );
            }
            else
            {
                aAction();
            }
            return;
        }
    }
}
