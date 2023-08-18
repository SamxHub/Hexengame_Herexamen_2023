using BoardSystem;
using GameSystem.Views;
using CardSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameSystem.GameState
{
    class PlayState : State
    {
        private Engine _engine;
        private Board _board;
        private PieceView _playerPiece;
        private BoardView _boardView;
        private DeckView _deck;
        public void InitializeScene(UnityEngine.AsyncOperation obj)
        {
            _boardView = GameObject.FindObjectOfType<BoardView>();
            _board = new(3);

            _boardView.PositionDrop += Dropped;
            _boardView.HoverEnter += TileHoverEnter;
            _boardView.HoverExit += TileHoverExit;

            _engine = new(_board);

            var pieceViews = GameObject.FindObjectsOfType<PieceView>();
            foreach (PieceView p in pieceViews)
            {
                if (p.Player == Player.Player)
                    _playerPiece = p;

                _board.Place(p.GridPosition, p);
            }

            _board.Moved += (s, e) => e.PieceView.MoveTo(e.ToPosition);
            _board.Taken += (s, e) => e.PieceView.Take();
            _board.Placed += (s, e) => e.PieceView.Place(e.PlacedPosition);

            _deck = GameObject.FindObjectOfType<DeckView>();
        }
        public override void OnEnter()
        {
            var asyncOperation = SceneManager.LoadSceneAsync("Cards", LoadSceneMode.Additive);
            asyncOperation.completed += InitializeScene;
        }
        public override void OnExit()
        {
            SceneManager.UnloadSceneAsync("Game");
        }
        public override void OnSuspend()
        {
            if (_boardView != null)
            {
                _boardView.PositionDrop -= Dropped;
                _boardView.HoverEnter -= TileHoverEnter;
                _boardView.HoverExit -= TileHoverExit;
            }
        }
        public override void OnResume()
        {
            if (_boardView != null)
            {
                _boardView.PositionDrop -= Dropped;
                _boardView.PositionDrop += Dropped;

                _boardView.HoverEnter -= TileHoverEnter;
                _boardView.HoverEnter += TileHoverEnter;

                _boardView.HoverExit -= TileHoverExit;
                _boardView.HoverExit += TileHoverExit;
            }
        }
        private void Dropped(object sender, PositionEventArgs eventArgs)
        {
            var positionHover = eventArgs.Position;
            var card = eventArgs.Card;

            if (_engine.DoAction(_playerPiece.GridPosition, positionHover))
                _deck.DeactivateCrd(card);

            _boardView.ActivatedPositions = null;
        }
        private void TileHoverEnter(object sender, PositionEventArgs eventArgs)
        {
            var positionHover = eventArgs.Position;
            var Card = eventArgs.Card;

            var tiles = _engine.ValidPosition(Card, _playerPiece.GridPosition, positionHover);

            if (tiles == null)
                return;

            if (tiles.ValidPositions.Contains(positionHover))
            {
                _engine.GetActionPositions(positionHover);
                _boardView.ActivatedPositions = tiles.ActionPositions;
            }
            else
                _boardView.ActivatedPositions = tiles.ValidPositions;
        }
        private void TileHoverExit(object sender, PositionEventArgs eventArgs) =>
           _boardView.ActivatedPositions = null;

    }
}
