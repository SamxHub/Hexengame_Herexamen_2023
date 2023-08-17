using GameSystem.Views;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameSystem.GameStates
{
    //a class named StartState that inherits from the base class State
    public class StartState : State
    {
        private StartView _startView; // Declare a private variable to hold a reference to StartView
        
        // Override the OnEnter method of the base class
        public override void OnEnter()
        {
            // Load the "Start" scene asynchronously and add it to the current scene
            var asyncOperation = SceneManager.LoadSceneAsync("Start", LoadSceneMode.Additive);

            // Attach a callback function (InitializeScene) to the completed event of the async operation.
            /// This function will be automatically called when the 'complete' event is triggered
            asyncOperation.completed += InitializeScene;
        }

        // Callback function to initialize the scene after loading is complet
        private void InitializeScene(UnityEngine.AsyncOperation obj)
        {
            // Find and store a reference to the StartView object in the scene
            _startView = GameObject.FindObjectOfType<StartView>();
            // If the StartView is found, attach a handler for the PlayClicked event
            if (_startView != null)
                _startView.PlayClicked += OnPlayClicked;
        }

        // Event handler for the PlayClicked event of the StartView
        private void OnPlayClicked(object sender, EventArgs e) =>
            // Move the game's state machine to the "Play" state
            StateMachine.MoveTo(States.Play);

        // Override the OnExit method of the base class
        public override void OnExit()
        { 
            // Checks if there is a _startView
            ///follows a principle called "early return" or "guard clause."
            if (_startView == null)
                return;
            //If there is a startView, detach the handler for the PlayClicked event
            _startView.PlayClicked -= OnPlayClicked;

            // Unload the "Start" scene asynchronously
            SceneManager.UnloadSceneAsync("Start");
        }
    }
}


