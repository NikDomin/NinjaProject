using System.Threading.Tasks;
using Assets.Scripts.Agent.Player.PlayerStateMachine;
using Movement;
using UnityEngine;
using Utils;

namespace Agent.Player.PlayerStateMachine.States
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

    interface IOnCeilingState
    {
        public void TryOnCeilingSwitching();
    }

    interface IFlyState
    {
        public void TryFlySwitching();
    }

    interface IAttackState
    {
        public void TryAttackSwitching();
    }

    public class RunState : BasedState, IJumpState, IOnWallState, IFlyState
    {
        private PlayerState playerState;
        private Assets.Scripts.Agent.Player.PlayerStateMachine.PlayerStateMachine stateMachine;

        private bool isGrounded = false;


        public RunState(PlayerState player, Assets.Scripts.Agent.Player.PlayerStateMachine.PlayerStateMachine playerStateMachine) : base(player, playerStateMachine)
        {
            playerState = player;
            stateMachine = playerStateMachine;
        }

        public override void EnterState()
        {
            base.EnterState();

            StateName = "Run State";
            
            playerState.SwipeDetection.OnSwipe += TryJumpSwitching;
            playerState.PlayerAnimator.Anim.SetBool(playerState.PlayerAnimator.IsRunningKey, true);

            playerState.LandingTrigger();
            
            playerState.SwipeDetection._rigidbody2D.gravityScale = 1;
            playerState.MovementComponent._rigidbody2D.velocity = Vector3.zero;
            
            AgentUtils.NormalSpriteDirection(playerState.transform);
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
            playerState.PlayerAnimator.Anim.SetBool(playerState.PlayerAnimator.IsRunningKey, false);

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

           
            //playerState.MovementComponent._rigidbody2D.AddForce(Vector2.right * playerState.MovementComponent.Speed * Time.deltaTime, ForceMode2D.Force);
            playerState.MovementComponent._rigidbody2D.velocity =
                new Vector2(playerState.MovementComponent.Speed * Time.deltaTime, 0);

            
        }

        public void TryJumpSwitching()
        {
            playerState.StateMachine.ChangeState(playerState.JumpState);
        }

        public void TryOnWallSwitching()
        {
            // if (PositionCheck.ObstacleCheck(playerState.transform.TransformPoint(playerState.MovementComponent.WallCheckPosition), playerState.transform.right, playerState.MovementComponent.WallRayLength,
            //         playerState.MovementComponent.GroundLayer) && 
            //     !PositionCheck.PlatformCheck(playerState.transform.TransformPoint(playerState.MovementComponent.WallCheckPosition),
            //         playerState.transform.right, playerState.MovementComponent.WallRayLength, playerState.MovementComponent.GroundLayer))
            // {
            //     playerState.StateMachine.ChangeState(playerState.OnWallState);
            // }
            var colliders = playerState.MovementComponent.WallDetection.OverlapBox();
            if(colliders.Length != 0)
                playerState.StateMachine.ChangeState(playerState.FlyState);
        }

        public void TryFlySwitching()
        {
            //if dont on ground
            if (!PositionCheck.GroundCheck(playerState.transform.TransformPoint(playerState.MovementComponent.GroundCheckPosition),
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