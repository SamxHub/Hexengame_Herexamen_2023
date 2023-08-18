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
        private List<Position> _actionPositions;

        public Engine(Board board)
        {
            _board = board;
            _actionPositions = new();
        }
        private void GetValidPositions(CardView card, Position hovPos, Position playerPos)
        {
            if (card == null) return;
           
            if(card.GetType == CardType.Teleport)  
                _moveSet = new TeleportCard(_board, hovPos, playerPos);
            
            if (card.GetType == CardType.Line) 
                _moveSet = new LaserCard(_board, hovPos, playerPos);
           
            if (card.GetType == CardType.Swing) 
                _moveSet = new SwingCard(_board, hovPos, playerPos);
          
            if (card.GetType == CardType.Push) 
                _moveSet = new PushCard(_board, hovPos, playerPos);
           
            if (card.GetType == CardType.Meteor) 
                _moveSet = new MeteorCard(_board, hovPos, playerPos);
        }
        public void GetActionPositions(Position position) => _moveSet.GetActionPositions(horPos);
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
