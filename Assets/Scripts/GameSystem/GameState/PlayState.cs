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
        }
        public override void OnEnter()
        {
           
        }
        public override void OnExit()
        {
            base.OnExit();
        }
        public override void OnSuspend()
        {
            base.OnSuspend();
        }
        public override void OnResume()
        {
            base.OnResume();
        }
        private void Dropped(object sender/*,PositionEventArgs eventArgs*/)
        {

        }
        private void TileHoverEnter(object sender/*,PositionEventArgs eventArgs*/)
        {

        }
        private void TIleHoverExit(object sender/*,PositionEventArgs eventArgs*/)
        {

        }
    }
}
