using BoardSystem;
using GameSystem.Views;
using CardSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameSystem.GameStates
{
    // Define a class named PlayState that inherits from the base class State
    public class PlayState : State
    {
        // Declare private variables to hold various game elements
        private Engine _engine;
        private Board _board;
        private PieceView _playerPiece;
        private BoardView _boardView;
        private DeckView _deck;

        // Callback function to initialize the scene after loading is complete
        public void InitializeScene(UnityEngine.AsyncOperation obj)
        {
            // Find and store references to BoardView and create a new Board instance
            _boardView = GameObject.FindObjectOfType<BoardView>();
            _board = new Board(3);

            // Attach event handlers for interactions with tiles on the board
            _boardView.PositionDrop += Dropped;
            _boardView.HoverEnter += TileEnterHover;
            _boardView.HoverExit += TileExitHover;

            // Create an instance of the Engine class, passing the Board instance
            _engine = new Engine(_board);

            // Find all PieceView instances in the scene and iterate through them
            var pieceViews = GameObject.FindObjectsOfType<PieceView>();
            foreach (PieceView p in pieceViews)
            {
                // If the player's piece is found, store its reference
                if (p.Player == Player.Player)
                    _playerPiece = p;

                // Place each piece on the board at its corresponding grid position
                _board.Place(p.GridPosition, p);
            }

            // Attach lambda expressions to Board events to handle piece movements
            _board.Moved += (s, e) => e.PieceView.MoveTo(e.ToPosition);
            _board.Taken += (s, e) => e.PieceView.Take();
            _board.Placed += (s, e) => e.Pieceview.Place(e.PlacePositition);

            // Find and store a reference to the DeckView instance in the scene
            _deck = GameObject.FindObjectOfType<DeckView>();
        }

        // Override the OnEnter method of the base class
        public override void OnEnter()
        {
            // Load the "Cards" scene asynchronously and add it to the current scene
            var asyncOperation = SceneManager.LoadSceneAsync("Cards", LoadSceneMode.Additive);
            asyncOperation.completed += InitializeScene;
        }

        // Override the OnExit method of the base class
        public override void OnExit() =>
            // Unload the "Game" scene asynchronously
            SceneManager.UnloadSceneAsync("Game");

        // Override the OnSuspend method of the base class
        public override void OnSuspend()
        {
            // Detach event handlers for PositionDrop, HoverEnter, and HoverExit events
            if (_boardView != null)
            {
                _boardView.PositionDrop -= Dropped;
                _boardView.HoverEnter -= TileEnterHover;
                _boardView.HoverExit -= TileExitHover;
            }
        }
        // Override the OnResume method of the base class
        public override void OnResume()
        {
            // Reattach event handlers for PositionDrop, HoverEnter, and HoverExit events
            if (_boardView != null)
            {
                _boardView.PositionDrop -= Dropped;
                _boardView.PositionDrop += Dropped;

                _boardView.HoverEnter -= TileEnterHover;
                _boardView.HoverEnter += TileEnterHover;

                _boardView.HoverExit -= TileExitHover;
                _boardView.HoverExit += TileExitHover;
            }
        }
        // Event handler for the PositionDrop event of the BoardView
        private void Dropped(object sender, PositionEventArgs eventArgs)
        {
            // Retrieve the position and card from the event arguments
            var positionHover = eventArgs.Position;
            var card = eventArgs.Card;

            // Deactivate the card if the engine allows it
            if (_engine.DoAction(_playerPiece.GridPosition, positionHover))
                _deck.DeactivateCard(card);

            // Clear the activated positions on the board
            _boardView.ActivatedPositions = null;
        }
        // Event handler for the HoverEnter event of the BoardView
        private void TileEnterHover(object sender, PositionEventArgs eventArgs)
        {
            // Retrieve the position and card from the event arguments
            var positionHover = eventArgs.Position;
            var card = eventArgs.Card;

            // Calculate valid positions based on the card and player's piece position
            var tiles = _engine.ValidPosition(card, _playerPiece.GridPosition, positionHover);

            ///follows a principle called "early return" or "guard clause."
            if (tiles == null)
                return;


            // If the hover position is valid for an action, show possible action positions
            if (tiles.ValidPositions.Contains(positionHover))
            {
                _engine.GetActionPositions(positionHover);
                _boardView.ActivatedPositions = tiles.ActionPositions;
            }

            // If not a valid action position, show valid movement positions
            else
                _boardView.ActivatedPositions = tiles.ValidPositions;
        }
        // Event handler for the HoverExit event of the BoardView
        private void TileExitHover(object sender, PositionEventArgs eventArgs) =>
            // Clear the activated positions on the board when hover exits
            _boardView.ActivatedPositions = null;

    }
}

