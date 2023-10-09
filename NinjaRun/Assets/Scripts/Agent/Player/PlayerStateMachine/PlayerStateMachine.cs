using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Agent.Player.PlayerStateMachine
{
    public class PlayerStateMachine 
    {
        public BasedState CurrentState { get; set; }

        public void Initialize(BasedState startingState)
        {
            CurrentState = startingState;
            CurrentState.EnterState();
        }

        public void ChangeState(BasedState newState)
        {
            CurrentState.ExitState();
            CurrentState = newState;
            CurrentState.EnterState();
        }
    }
}