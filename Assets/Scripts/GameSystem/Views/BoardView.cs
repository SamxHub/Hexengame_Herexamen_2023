using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameSystem.Views
{
    public class PositionEventArgs : EventArgs
    {
        public Position Position { get; }
        public CardView Card { get; }
        public PositionEventArgs(Position pos, CardView card)
        {
            Position = pos;
            Card = card;
        }
    }
    class BoardView
    {
    }
}
