using Assets.Scripts.Agent.Player.PlayerStateMachine;
using Assets.Scripts.Agent.Player.PlayerStateMachine.States;
using Movement;
using UnityEngine;

namespace Agent.Player.PlayerStateMachine.States
{
    public class OnWallState : BasedState, IJumpState, IFlyState
    {
        private PlayerState playerState;
        private Assets.Scripts.Agent.Player.PlayerStateMachine.PlayerStateMachine stateMachine;

        
        public OnWallState(PlayerState player, Assets.Scripts.Agent.Player.PlayerStateMachine.PlayerStateMachine playerStateMachine) : base(player, playerStateMachine)
        {
            playerState = player;
            stateMachine = playerStateMachine;
        }

        public override void EnterState()
        {
            base.EnterState();
            playerState.SwipeDetection.OnSwipe += TryJumpSwitching;
            playerState.PlayerAnimator.Anim.SetTrigger(playerState.PlayerAnimator.OnWallTriggerKey);

            playerState.LandingTrigger();
            
            playerState.MovementComponent._rigidbody2D.gravityScale = 0;
            playerState.MovementComponent._rigidbody2D.velocity = Vector3.zero;
            playerState.MovementComponent._rigidbody2D.angularVelocity = 0;
        }

        public override void ExitState()
        {
            base.ExitState();
            playerState.SwipeDetection.OnSwipe -= TryJumpSwitching;

            playerState.PlayerAnimator.Anim.SetTrigger(playerState.PlayerAnimator.EndWallTriggerKey);
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
                //dont on wall check
                if (!PositionCheck.ObstacleCheck(playerState.transform.TransformPoint(playerState.MovementComponent.WallCheckPosition), playerState.transform.right, playerState.MovementComponent.WallRayLength,
                        playerState.MovementComponent.GroundLayer))
                {
                    playerState.StateMachine.ChangeState(playerState.FlyState);
                }

            }
        }
    }
}