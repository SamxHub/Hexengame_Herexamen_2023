using BoardSystem;
using GameSystem.Views;
using System.Collections.Generic;

namespace CardSystem
{
    public class TeleportCard : MoveSet
    {
        public TeleportCard(Board board, Position hovPos, Position playerPos) : base(board, hovPos, playerPos)
            => GetValidPositions(HoverPosition);
        public override void GetActionPositions(Position position)
        {
            ActionPositions.Clear();
            if (!Board.Pieces.TryGetValue(position, out PieceView piece))
                ActionPositions.Add(position);
        }
        public override void GetValidPositions(Position position)
        => ValidPositions.AddRange(new MoveSetHelper(PlayerPosition, Board)
                .Everything().ValidPositions());
        public override List<Position> Positions(Position playerPosition)
        {
            GetValidPositions(playerPosition);
            return ValidPositions;
        }
        internal override bool Execute(Position playerPos, Position hoverPos)
        {
            var validPos = Positions(playerPos);
            if (!validPos.Contains(hoverPos))
                return false;
            Board.Move(playerPos, hoverPos);
            return true;
        }
    }
}
