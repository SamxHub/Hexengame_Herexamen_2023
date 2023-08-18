namespace GameSystem.GameState
{
    public abstract class State //Base class representing a game state's behavior
    {
        public StateMachine StateMachine { get; set; }
        public virtual void OnEnter() { }
        public virtual void OnExit() { }
        public virtual void OnSuspend() { }
        public virtual void OnResume() { }
    }
}
