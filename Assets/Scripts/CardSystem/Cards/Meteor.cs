using BoardSystem;
using System.Collections.Generic;

namespace CardSystem.Cards
{
    public class MeteorCard : MoveSet
    {
        public MeteorCard(Board board, Position hoverPos, Position playerPos) : base(board, hoverPos, playerPos)
        {
            GetValidPositions(HoverPosition);
        }
        public override void GetActionPositions(Position hoverPosition)
        {
            ActionPositions.Clear();

            if (ValidPositions.Contains(hoverPosition))
            {
                ActionPositions.Add(hoverPosition);

                ActionPositions.AddRange(new MoveSetHelper(hoverPosition, Board)
                    .NorthEast(1)
                    .East(1)
                    .SouthEast(1)
                    .SouthWest(1)
                    .West(1)
                    .NorthWest(1)
                    .ValidPositions()
                    );
            }
        }

        public override void GetValidPositions(Position position)
            => ValidPositions.AddRange(new MoveSetHelper(PlayerPosition, Board)
                .Everything()
                .ValidPositions());           

        public override List<Position> Positions(Position playerPosition)
        {
            GetValidPositions(playerPosition);
            return ValidPositions;
        }
    }
}
