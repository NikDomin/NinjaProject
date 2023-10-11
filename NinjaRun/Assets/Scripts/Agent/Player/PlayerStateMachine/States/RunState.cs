using System.Collections;
using System.Threading.Tasks;
using Assets.Scripts.Movement;
using UnityEngine;

namespace Assets.Scripts.Agent.Player.PlayerStateMachine.States
{
    interface IJumpState
    {
        public void TryJumpSwitching();
    }

    interface IRunState
    {
        public void TryRunSwitching();
    }

    interface IOnWallState
    {
        public void TryOnWallSwitching();
    }

    interface IFlyState
    {
        public void TryFlySwitching();
    }

    public class RunState : BasedState, IJumpState, IOnWallState, IFlyState
    {
        private PlayerState playerState;
        private PlayerStateMachine stateMachine;

        private bool isGrounded = false;


        public RunState(PlayerState player, PlayerStateMachine playerStateMachine) : base(player, playerStateMachine)
        {
            playerState = player;
            stateMachine = playerStateMachine;
        }

        public override void EnterState()
        {
            base.EnterState();
            playerState.SwipeDetection.OnSwipe += TryJumpSwitching;

            playerState.SwipeDetection._rigidbody2D.gravityScale = 1;

            //playerState.MovementComponent._rigidbody2D.simulated = false;

            playerState.MovementComponent._rigidbody2D.velocity = Vector3.zero;
            playerState.MovementComponent._rigidbody2D.angularVelocity = 0;
            //SimulatedCD();

        }

        private async void SimulatedCD()
        {
            await Task.Delay(100);
            playerState.MovementComponent._rigidbody2D.simulated = true;
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

            //isGrounded = PositionCheck.GroundCheck(playerState.transform.TransformPoint(playerState.MovementComponent.GroundCheckPosition),
            //    playerState.MovementComponent.GroundCheckRadius, playerState.MovementComponent.GroundLayer);
            TryOnWallSwitching();
            TryFlySwitching();

            //playerState.MovementComponent._rigidbody2D.velocity =
            //    playerState.transform.right * playerState.MovementComponent.Speed * Time.deltaTime;
            playerState.MovementComponent._rigidbody2D.AddForce(playerState.transform.right * playerState.MovementComponent.Speed * Time.deltaTime);
        }

        public void TryJumpSwitching()
        {
            playerState.StateMachine.ChangeState(playerState.JumpState);
        }

        public void TryOnWallSwitching()
        {
            if (PositionCheck.WallCheck(playerState.transform.TransformPoint(playerState.MovementComponent.WallCheckPosition), playerState.transform.right, playerState.MovementComponent.RayLenght,
                    playerState.MovementComponent.GroundLayer))
            {
                playerState.StateMachine.ChangeState(playerState.OnWallState);
            }
        }

        public void TryFlySwitching()
        {
            //if dont on ground
            if (!PositionCheck.GroundCheck(playerState.transform.TransformPoint(playerState.MovementComponent.GroundCheckPosition),
                playerState.MovementComponent.GroundCheckRadius, playerState.MovementComponent.GroundLayer))
            {
                //dont on wall check
                if (!PositionCheck.WallCheck(playerState.transform.TransformPoint(playerState.MovementComponent.WallCheckPosition), playerState.transform.right, playerState.MovementComponent.RayLenght,
                        playerState.MovementComponent.GroundLayer))
                {
                    playerState.StateMachine.ChangeState(playerState.FlyState);
                }

            }
        }
    }
}