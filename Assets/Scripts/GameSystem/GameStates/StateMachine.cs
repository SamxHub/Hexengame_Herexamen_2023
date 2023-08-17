using System.Collections.Generic;

namespace GameSystem.GameStates
{
    public enum States
    {
        Start, // the start screen
        Play //the actual game
    }

    public class StateMachine //Managing state transitions in the game
    {
        private Dictionary<States, State> _states = new(); //Dictionary to store the mapping (relationship) between state names and state instances
        private Stack<States> _currentStateNames = new(); //Stack to keep track of the current state names

        public State CurrentState => _states[_currentStateNames.Peek()]; // Get the current state instance based on the top state name from the stack

        public void Register(States stateName, State state) // Method to register a state in the state machine
        {
            state.StateMachine = this; // Set the state machine reference in the state

            _states.Add(stateName, state); // Add the state to the dictionary
        }

        public States InitialState // Property to set the initial state of the state machine
        {
            set
            {
                _currentStateNames.Push(value); // Push the initial state onto the stack
                CurrentState.OnEnter(); // Trigger the OnEnter method of the current state
                CurrentState.OnResume(); // Trigger the OnResume method of the current state
            }
        }

        ///This method essentially replaces the current state on the stack with a new state.
        public void MoveTo(States stateName) // Method to  transition from the current state to a completely new state.
        {
            CurrentState.OnSuspend(); // Trigger the OnSuspend method of the current state
            CurrentState.OnExit(); // Trigger the OnExit method of the current state

            _currentStateNames.Pop(); // Remove the current state name from the stack
            _currentStateNames.Push(stateName); // Push the new state onto the stack

            CurrentState.OnEnter(); // Trigger the OnEnter method of the new current state
            CurrentState.OnResume(); // Trigger the OnResume method of the new current state
        }

        ///This new state represents a temporary overlay or a sub-state within the current state.
        public void Push(States stateName) // add a new state on top of the current state.
        {
            CurrentState.OnSuspend(); // Trigger the OnSuspend method of the current state

            _currentStateNames.Push(stateName); // Push the new state onto the stack

            CurrentState.OnEnter(); // Trigger the OnEnter method of the new current state
            CurrentState.OnResume(); // Trigger the OnResume method of the new current state
        }

        public void Pop() // Method to pop the current state from the stack
        {
            CurrentState.OnSuspend(); // Trigger the OnSuspend method of the current state
            CurrentState.OnExit(); // Trigger the OnExit method of the current state

            _currentStateNames.Pop(); // Remove the current state name from the stack

            CurrentState.OnResume(); // Trigger the OnResume method of the new current state
        }
    }
}
