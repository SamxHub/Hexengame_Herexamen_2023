using System.Collections.Generic;

namespace GameSystem.GameState
{
    public enum GameStates
    {
        Start,
        Play,
        End
    }
    public class StateMachine
    {
        private Dictionary<GameStates, State> _states = new();
        private Stack<GameStates> _currentStateNames = new();

        public State CurrentState => _states[_currentStateNames.Peek()];

        public void Register(GameStates stateName, State state)
        {
            state.StateMachine = this;
            _states.Add(stateName, state);
        }
        public GameStates InitialState
        {
            set
            {
                _currentStateNames.Push(value);
                CurrentState.OnEnter();
                CurrentState.OnResume();
            }
        }
        public void MoveTo(GameStates stateName)
        {
            CurrentState.OnSuspend();
            CurrentState.OnExit();

            _currentStateNames.Pop();
            _currentStateNames.Push(stateName);

            CurrentState.OnEnter();
            CurrentState.OnResume();
        }
        public void Push(GameStates stateName)
        {
            CurrentState.OnSuspend();

            _currentStateNames.Push(stateName);

            CurrentState.OnEnter();
            CurrentState.OnResume();
        }
        public void Pop()
        {
            CurrentState.OnSuspend();
            CurrentState.OnExit();

            _currentStateNames.Pop();

            CurrentState.OnResume();
        }

    }
}
