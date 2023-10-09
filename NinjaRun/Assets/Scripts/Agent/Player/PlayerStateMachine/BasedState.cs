using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Agent.Player.PlayerStateMachine
{
    public class BasedState
    {
        protected PlayerState player;
        protected PlayerStateMachine playerStateMachine;

        public BasedState(PlayerState player, PlayerStateMachine playerStateMachine)
        {
            this.player = player;
            this.playerStateMachine = playerStateMachine;
        }

        public virtual void EnterState(){}

        public virtual void ExitState() {}

        public virtual void FrameUpdate() {}
        
        public virtual void PhysicUpdate(){}
    }
}