using System.Collections;
using Assets.Scripts.Movement;
using UnityEngine;

namespace Assets.Scripts.Agent.Player.PlayerStateMachine.States
{
    public class JumpState : BasedState, IFlyState
    {
        private PlayerState playerState;
        private PlayerStateMachine stateMachine;

        public JumpState(PlayerState player, PlayerStateMachine playerStateMachine) : base(player, playerStateMachine)
        {
            playerState = player;
            stateMachine = playerStateMachine;
        }

        public override void EnterState()
        {
            base.EnterState();

            Jump();

            TryFlySwitching();
        }

        public override void ExitState()
        {
            base.ExitState();
        }

        private void Jump()
        {
            Vector2 normalizedDirection = new Vector2(playerState.SwipeDetection.directionSwipe.x,
                playerState.SwipeDetection.directionSwipe.y).normalized;

            //Add force
            //Add force 
            playerState.SwipeDetection._rigidbody2D.gravityScale = 1;
            if (playerState.SwipeDetection.forceType == ForceType.withoutDrag)
                playerState.SwipeDetection._rigidbody2D.AddForce(playerState.SwipeDetection.directionSwipe * playerState.SwipeDetection.impactsStrength, ForceMode2D.Impulse);
            if (playerState.SwipeDetection.forceType == ForceType.withDrag)
            {
                playerState.SwipeDetection._rigidbody2D.velocity = Vector3.zero;
                playerState.SwipeDetection._rigidbody2D.angularVelocity = 0;

                playerState.SwipeDetection._rigidbody2D.AddForce(playerState.SwipeDetection.directionSwipe * playerState.SwipeDetection.impactsStrength, ForceMode2D.Impulse);
            }
        }

        public override void FrameUpdate()
        {
            base.FrameUpdate();
        }

        public override void PhysicUpdate()
        {
            base.PhysicUpdate();
        }

        public void TryFlySwitching()
        {
            playerState.StateMachine.ChangeState(playerState.FlyState);
        }
    }
}