using GameSystem.Views;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameSystem.GameState
{
    class StartState : State
    {
        private StartView _startView;
        public override void OnEnter()
        {
            var asyncOp = SceneManager.LoadSceneAsync("Start", LoadSceneMode.Additive);

            asyncOp.completed += InitializeScene;
        }
        private void InitializeScene(UnityEngine.AsyncOperation operation)
        {
            _startView = GameObject.FindObjectOfType<StartView>();
            if (_startView == null) return;

            _startView.PlayClicked += OnPlayClicked;
        }
        private void OnPlayClicked(object sender, EventArgs eventArgs)
            => StateMachine.MoveTo(GameStates.Play);
        public override void OnExit()
        {
            if (_startView == null)
                return;
            _startView.PlayClicked -= OnPlayClicked;

            SceneManager.UnloadSceneAsync("Start");
        }
    }
}
