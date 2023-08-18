using BoardSystem;
using GameSystem.Views;
using System.Collections.Generic;
using UnityEngine;

namespace CardSystem
{
    public abstract class MoveSet
    {
        public List<Position> ValidPositions { get; set; }
        public List<Position> ActionPositions { get; set;  }

        public Board Board { get; set; }
        public Position HoverPosition { get; set; }
        public Position PlayerPosition { get; set; }
        protected MoveSet(Board board, Position hoverPos, Position playerPos)
        {
            Board = board;
            HoverPosition = hoverPos;
            PlayerPosition = playerPos;
            ValidPositions = new List<Position>();
            ActionPositions = new List<Position>();
        }
        public abstract void GetActionPositions(Position position);
        public abstract void GetValidPositions(Position position);
        public abstract List<Position> Positions(Position playerPosition);
        internal virtual bool Execute(Position playerPos, Position hoverPos)
        {
            var validPositions = Positions(playerPos);
            if (!validPositions.Contains(hoverPos))
                return false;

            GetActionPositions(hoverPos);

            bool hasPieceBeenTaken = false;
            List<PieceView> takenPieces = new();
            List<Position> positionTakenPieces = new();

            foreach(Position pos in ActionPositions)
            {
                if(Board.Pieces.TryGetValue(pos, out PieceView piece))
                {
                    if(piece.Player == Player.Enemy)
                    {
                        takenPieces.Add(piece);
                        positionTakenPieces.Add(pos);
                        hasPieceBeenTaken = true;
                    }
                }
            }
            if(hasPieceBeenTaken)
                foreach(PieceView pieceView in takenPieces)
                {
                    int indexPos = takenPieces.IndexOf(pieceView);
                    Board.Take(positionTakenPieces[indexPos]);
                }
            return true;
        }
        public Vector2Int Normalixe(Vector2Int direction)
        {
            if (direction.x > 1) direction = new Vector2Int(1, direction.y);
            
            if (direction.x < -1) direction = new Vector2Int(-1, direction.y);
            
            if (direction.x < -1) direction = new Vector2Int(direction.x, -1);
            
            if (direction.x > 1) direction = new Vector2Int(direction.x, 1);

            
            return direction;
        }
    }
}