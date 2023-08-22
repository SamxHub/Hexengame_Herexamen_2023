using BoardSystem;
using GameSystem.Views;
using CardSystem.Cards;
using System.Collections.Generic;

namespace CardSystem
{ 
    class Engine
    {
        private Board _board;
        private MoveSet _moveSet;

        public Engine(Board board)
        {
            _board = board;
        }
        private void GetValidPositions(CardView card, Position hovPos, Position playerPos)
        {
            if(card != null)
            {
                if (card.Type == CardType.Teleport)
                {
                    _moveSet = new TeleportCard(_board, hovPos, playerPos);
                }
                else if (card.Type == CardType.Line)
                {
                    _moveSet = new LaserCard(_board, hovPos, playerPos);
                }
                else if (card.Type == CardType.Swing)
                {
                    _moveSet = new SwingCard(_board, hovPos, playerPos);
                }
                else if (card.Type == CardType.Push)
                {
                    _moveSet = new PushCard(_board, hovPos, playerPos);
                }
                else if (card.Type == CardType.Meteor)
                {
                    _moveSet = new MeteorCard(_board, hovPos, playerPos);
                }
            }
        }
        public void GetActionPositions(Position position) => _moveSet.GetActionPositions(position);
        internal MoveSet ValidPosition(CardView card, Position gridPosition, Position horPos)
        {
            GetValidPositions(card, horPos, gridPosition);
            return _moveSet;
        }
        public bool DoAction(Position playerPos, Position hovPos)
        {
            if (!_moveSet.Execute(playerPos, hovPos))
                return false;

            return true;
        }
    }
}
