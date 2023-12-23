using System;
using System.Collections;
using Agent.Player.PlayerStateMachine;
using Agent.Player.PlayerStateMachine.States;
using Movement;
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
            playerState.PlayerAnimator.Anim.SetTrigger(playerState.PlayerAnimator.StartJumpKey);

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
                if (playerState.SwipeDetection.JumpType == JumpType.normalization)
                {
                    playerState.SwipeDetection._rigidbody2D.velocity = Vector3.zero;
                    playerState.SwipeDetection._rigidbody2D.angularVelocity = 0;

                    //playerState.SwipeDetection._rigidbody2D.AddForce(/*playerState.SwipeDetection.directionSwipe*/normalizedDirection * playerState.SwipeDetection.impactsStrength, ForceMode2D.Impulse);
                    playerState.SwipeDetection._rigidbody2D.velocity = new Vector2(
                        normalizedDirection.x * playerState.SwipeDetection.impactsStrength,
                        normalizedDirection.y * playerState.SwipeDetection.impactsStrength);
                }
                else
                {
                    playerState.SwipeDetection._rigidbody2D.velocity = Vector3.zero;
                    playerState.SwipeDetection._rigidbody2D.angularVelocity = 0;

                    Vector2 direction = new Vector2(playerState.SwipeDetection.directionSwipe.x,
                        playerState.SwipeDetection.directionSwipe.y);

                    

                    Vector2 absDirection = new Vector2(Math.Abs(playerState.SwipeDetection.directionSwipe.x),
                        Math.Abs(playerState.SwipeDetection.directionSwipe.y));

                    if (absDirection.x > 15)
                    {
                        if (direction.x > 0)
                            direction.x = 15;
                        else if (direction.x < 0)
                            direction.x = -15;
                    }
                    if (absDirection.y > 15)
                    {
                        if (direction.y > 0)
                            direction.y = 15;
                        else if (direction.y < 0)
                            direction.y = -15;
                    }


                    playerState.SwipeDetection._rigidbody2D.velocity = direction;
                }
             
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