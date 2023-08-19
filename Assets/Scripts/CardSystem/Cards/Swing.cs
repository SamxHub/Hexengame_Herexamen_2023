using BoardSystem;
using System.Collections.Generic;

namespace CardSystem.Cards
{
    class SwingCard : MoveSet
    {
        public SwingCard(Board board, Position position, Position playerPosition) : base(board, hoverPos, playerPos)
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
            if(ValidPositions.Contains(HoverPosition))
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
    }
}
