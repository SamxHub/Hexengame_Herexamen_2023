using BoardSystem;
using GameSystem.Views;
using System.Collections.Generic;
using UnityEngine;

namespace CardSystem.Cards
{
    class PushCard : MoveSet
    {
        public PushCard(Board board, Position hoverPos, Position playerPos) : base(board, hoverPos, playerPos)
        {
            GetValidPositions(PlayerPosition);
            GetActionPositions(HoverPosition);
        }
        public override List<Position> Positions(Position playerPosition)
        {
            GetValidPositions(playerPosition);
            return ValidPositions;
        }
        public override void GetValidPositions(Position position)
        {
            ValidPositions.AddRange(new MoveSetHelper(PlayerPosition, Board)
                .NorthEast(1)
                .NorthWest(1)
                .SouthEast(1)
                .SouthWest(1)
                .East(1)
                .West(1)
                .ValidPositions());
        }
        public override void GetActionPositions(Position position)
        {
            ActionPositions.Clear();
            if (ValidPositions.Contains(HoverPosition))
            {
                ActionPositions.Add(HoverPosition);
                List<Position> neighbours = new();
                neighbours.AddRange(new MoveSetHelper(position, Board)
                    .NorthEast(1)
                    .East(1)
                    .SouthEast(1)
                    .SouthWest(1)
                    .West(1)
                    .NorthWest(1)
                    .ValidPositions()
                    );
                foreach (Position pos in neighbours)
                    if (ValidPositions.Contains(pos))
                        ActionPositions.Add(pos);
            }
        }
        internal override bool Execute(Position playerPos, Position hoverPos)
        {
            var validPos = Positions(playerPos);
            if (!ValidPositions.Contains(hoverPos))
                return false;

            GetActionPositions(hoverPos);

            bool hasPieceMoved = false;
            List<PieceView> movePieces = new();
            List<Position> posMovePieces = new();
            foreach(Position pos in ActionPositions)
                if(Board.Pieces.TryGetValue(pos, out PieceView piece))
                    {
                    movePieces.Add(piece);
                    posMovePieces.Add(pos);
                    hasPieceMoved = true;
                }
            if(hasPieceMoved)
                foreach(PieceView piece in movePieces)
                {
                    int indexPos = movePieces.IndexOf(piece);

                    Vector2Int dir =new((hoverPos.Q - playerPos.Q), (hoverPos.R - playerPos.R));
                    dir = Normalize(dir);

                    Position to = new(piece.GridPosition.Q + dir.x, piece.GridPosition.R + dir.y);

                    if (Board.IsValid(to))
                        Board.Move(posMovePieces[indexPos], to);
                    else
                        Board.Take(posMovePieces[indexPos]);
                }
            return true;
        }
    }
}
