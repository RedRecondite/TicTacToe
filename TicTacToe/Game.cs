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

        public MoveResult Move( int aIndex, Box aBox )
        {
            lock ( mLock )
            {
                if ( aIndex >= 0 && aIndex <= 8 && mBoxes[aIndex] == Box.Blank &&
                     ( ( mPhase == Phase.OTurn && aBox == Box.O ) ||
                       ( mPhase == Phase.XTurn && aBox == Box.X ) ) )
                {
                    mBoxes[aIndex] = aBox;

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
