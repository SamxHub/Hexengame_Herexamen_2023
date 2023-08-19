using BoardSystem;
using System.Collections.Generic;
using UnityEngine;

namespace CardSystem.Cards
{
    class LaserCard : MoveSet
    {
        public LaserCard(Board board, Position position, Position playerPos) : base(Board, hoverPos, playerPos)
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
                .NorthEast()
                .NorthWest()
                .SouthEast()
                .SouthWest()
                .East()
                .West()
                .ValidPositions());
        }
        public override void GetActionPositions(Position position)
        {
            ActionPositions.Clear();
            MoveSetHelper moveSetAction = new MoveSetHelper(PlayerPosition, Board);

            Vector2Int dir = new((HoverPosition.Q - PlayerPosition.Q), (HoverPosition.R - PlayerPosition.R));

            if (dir == new Vector2Int(0, 0))
                return;

            ActionPositions.AddRange(moveSetAction.Collect(dir).ValidPositions());
        }
    }
}
