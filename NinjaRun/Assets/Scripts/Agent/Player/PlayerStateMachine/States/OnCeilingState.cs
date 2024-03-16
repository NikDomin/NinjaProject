using System.Collections;
using Agent.Player.PlayerStateMachine;
using Agent.Player.PlayerStateMachine.States;
using Movement;
using UnityEngine;

namespace Assets.Scripts.Agent.Player.PlayerStateMachine.States
{
    public class OnCeilingState : BasedState, IJumpState, IFlyState
    {
        private PlayerState playerState;
        private PlayerStateMachine stateMachine;

        public OnCeilingState(PlayerState player, PlayerStateMachine playerStateMachine) : base(player, playerStateMachine)
        {
            playerState = player;
            stateMachine = playerStateMachine;
        }

        public override void EnterState()
        {
            base.EnterState();
            playerState.SwipeDetection.OnSwipe += TryJumpSwitching;
            playerState.PlayerAnimator.Anim.SetTrigger(playerState.PlayerAnimator.OnCeilTriggerKey);

            playerState.LandingTrigger();
            
            playerState.MovementComponent._rigidbody2D.gravityScale = 0;
            playerState.MovementComponent._rigidbody2D.velocity = Vector3.zero;
            playerState.MovementComponent._rigidbody2D.angularVelocity = 0;

        }

        public override void ExitState()
        {
            base.ExitState();

            playerState.SwipeDetection.OnSwipe -= TryJumpSwitching;

            playerState.PlayerAnimator.Anim.SetTrigger(playerState.PlayerAnimator.EndCeilTriggerKey);
        }

        public override void FrameUpdate()
        {
            base.FrameUpdate();
        }

        public override void PhysicUpdate()
        {
            base.PhysicUpdate();
            TryFlySwitching();
        }

        public void TryJumpSwitching()
        {
            playerState.StateMachine.ChangeState(playerState.JumpState);
        }

        public void TryFlySwitching()
        {
            //if dont on ground
            if (PositionCheck.GroundCheck(playerState.transform.TransformPoint(playerState.MovementComponent.GroundCheckPosition),
                    playerState.MovementComponent.GroundCheckRadius, playerState.MovementComponent.GroundLayer))
            {
                // //dont on wall check
                // if (!PositionCheck.ObstacleCheck(playerState.transform.TransformPoint(playerState.MovementComponent.WallCheckPosition), playerState.transform.right, playerState.MovementComponent.WallRayLength,
                //         playerState.MovementComponent.GroundLayer))
                // {
                //     playerState.StateMachine.ChangeState(playerState.FlyState);
                // }

                //dont on wall check
                var colliders = playerState.MovementComponent.WallDetection.OverlapBoxNonAlloc();
                if(colliders == 0)
                    playerState.StateMachine.ChangeState(playerState.FlyState);
            }
        }
    }
}