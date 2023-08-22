using GameSystem.GameState;
using UnityEngine;

namespace GameSystem
{
    public class GameLoop : MonoBehaviour
    {
        private StateMachine _stateMachine;

        void Start()
        {
            _stateMachine = new();
            _stateMachine.Register(GameStates.Play, new PlayState());
            _stateMachine.Register(GameStates.Start, new StartState());
            //_stateMachine.Register(GameStates.End, new EndState());

            _stateMachine.InitialState = GameStates.Start;
        }
    }
}
