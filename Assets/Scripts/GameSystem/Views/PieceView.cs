using BoardSystem;
using CardSystem;
using UnityEngine;

namespace GameSystem.Views
{
    public class PieceView : MonoBehaviour, IPiece
    {
        [SerializeField] private Player _player;
        private Enemy enemy;
        public Position GridPosition => PositionHelper.GridPosition(transform.position);
        public Player Player => _player;
        private void Awake()
        {
            enemy = new();
            var gridPosition = PositionHelper.GridPosition(transform.position);
            transform.position = PositionHelper.WorldPosition(gridPosition);
        }
        internal void MoveTo(Position to) => transform.position = PositionHelper.WorldPosition(to);
        internal void Take()
        {
            gameObject.SetActive(false);
            enemy.ChangePosition();
        }
        internal void Place(Position position)
        {
            gameObject.SetActive(true);
            MoveTo(position);
        }
    }
}
