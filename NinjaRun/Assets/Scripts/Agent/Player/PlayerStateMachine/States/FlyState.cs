using System.Collections;
using System.Threading.Tasks;
using Assets.Scripts.Movement;
using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.Agent.Player.PlayerStateMachine.States
{
    public class FlyState : BasedState, IRunState, IOnWallState, IJumpState, IOnCeilingState
    {
        private PlayerState playerState;
        private PlayerStateMachine stateMachine;

        private bool isCanCling;
        private bool isCanRun;
        public FlyState(PlayerState player, PlayerStateMachine playerStateMachine) : base(player, playerStateMachine)
        {
            playerState = player;
            stateMachine = playerStateMachine;
        }

        public override void EnterState()
        {
            base.EnterState();
            isCanCling = false;
            isCanRun = false;
            DetectCdOnWall();
            DetectCdOnRun();


            playerState.SwipeDetection._rigidbody2D.gravityScale = 1;

            playerState.SwipeDetection.OnSwipe += TryJumpSwitching;
        }
        private async void DetectCdOnWall()
        {
            await Task.Delay(100);
            isCanCling = true;
        }
        private async void DetectCdOnRun()
        {
            await Task.Delay(100);
            isCanRun = true;
        }

        public override void ExitState()
        {
            base.ExitState();
            playerState.SwipeDetection.OnSwipe -= TryJumpSwitching;
        }

        public override void FrameUpdate()
        {
            base.FrameUpdate();
        }

        public override void PhysicUpdate()
        {
            base.PhysicUpdate();

            AgentUtils.SpriteDirection(playerState.transform, playerState.SwipeDetection.directionSwipe );

            TryRunSwitching();
            TryOnWallSwitching();
            TryOnCeilingSwitching();
        }

        public void TryRunSwitching()
        {
            if (PositionCheck.GroundCheck(playerState.transform.TransformPoint(playerState.MovementComponent.GroundCheckPosition),
                   playerState.MovementComponent.GroundCheckRadius, playerState.MovementComponent.GroundLayer) && isCanRun)
            {
                playerState.StateMachine.ChangeState(playerState.RunState);
            }

        }

        public void TryOnWallSwitching()
        {
            if (PositionCheck.ObstacleCheck(playerState.transform.TransformPoint(playerState.MovementComponent.WallCheckPosition), playerState.transform.right * playerState.transform.localScale.x, playerState.MovementComponent.WallRayLength,
                   playerState.MovementComponent.GroundLayer) && isCanCling)
            {
                playerState.StateMachine.ChangeState(playerState.OnWallState);
            }
        }

        public void TryJumpSwitching()
        {
            playerState.StateMachine.ChangeState(playerState.JumpState);
        }

        public void TryOnCeilingSwitching()
        {
            if (PositionCheck.ObstacleCheck(playerState.transform.TransformPoint(playerState.MovementComponent.CeilingCheckPosition), playerState.transform.up, playerState.MovementComponent.CeilRayLength, playerState.MovementComponent.GroundLayer) && isCanCling)
            {
                playerState.StateMachine.ChangeState(playerState.OnCeilingState);
            }
        }
    }
}