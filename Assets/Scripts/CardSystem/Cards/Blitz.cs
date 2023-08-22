using BoardSystem;
using GameSystem.Views;
using System.Collections.Generic;

namespace CardSystem.Cards
{
    class Blitz : MoveSet
    {
        public Blitz(Board board, Position position, Position playerPos) : base(board, position, playerPos)
        {
            GetValidPositions(PlayerPosition);
        }
        public override List<Position> Positions(Position playerPosition)
        {
            GetValidPositions(playerPosition);
            return ValidPositions;
        }
        public override void GetActionPositions(Position hoverPosition)
        {
            ActionPositions.Clear();

            if (ValidPositions.Contains(hoverPosition))
            {
                ActionPositions.Add(hoverPosition);

                ActionPositions.AddRange(new MoveSetHelper(hoverPosition, Board)
                    .NorthEast(2)
                    .East(2)
                    .SouthEast(2)
                    .SouthWest(2)
                    .West(2)
                    .NorthWest(2)
                    .ValidPositions());
            }
        }
        public override void GetValidPositions(Position position)
            => ValidPositions.AddRange(new MoveSetHelper(PlayerPosition, Board)
                .Everything()
                .ValidPositions());
        internal override bool Execute(Position playerPos, Position hoverPos)
        {
            var validPositions = Positions(playerPos);
            if (!validPositions.Contains(hoverPos))
                return false;

            GetActionPositions(hoverPos);

            bool hasPieceBeenTaken = false;
            List<PieceView> takenPieces = new();
            List<Position> positionTakenPieces = new();

            foreach (Position pos in ActionPositions)
            {
                if (Board.Pieces.TryGetValue(pos, out PieceView piece))
                {
                    if (piece.Player == Player.Enemy)
                    {
                        takenPieces.Add(piece);
                        positionTakenPieces.Add(pos);
                        hasPieceBeenTaken = true;
                    }
                }
            }
            if (hasPieceBeenTaken)
                Board.Take(positionTakenPieces[1]);
            return true;
        }
    }
}
