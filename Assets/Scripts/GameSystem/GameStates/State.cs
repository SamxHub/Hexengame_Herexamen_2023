namespace GameSystem.GameStates
{
    public abstract class State //Base class representing a game state's behavior
    {
        public StateMachine StateMachine { get; set; }

        public virtual void OnEnter() { } // Logic for entering the state

        public virtual void OnExit() { } // Logic for exiting the state

        public virtual void OnSuspend() { } // Logic for suspending the state

        public virtual void OnResume() { } // Logic for resuming the state
    }
}