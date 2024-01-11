using System.Threading.Tasks;
using Assets.Scripts.Agent.Player.PlayerStateMachine;
using Movement;
using Utils;

namespace Agent.Player.PlayerStateMachine.States
{
    public class FlyState : BasedState, IRunState, IOnWallState, IJumpState, IOnCeilingState, IAttackState
    {
        private PlayerState playerState;
        private Assets.Scripts.Agent.Player.PlayerStateMachine.PlayerStateMachine stateMachine;

        private bool isCanCling;
        private bool isCanRun;

        public FlyState(PlayerState player, Assets.Scripts.Agent.Player.PlayerStateMachine.PlayerStateMachine playerStateMachine) : base(player, playerStateMachine)
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
            TryAttackSwitching();
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
                   playerState.MovementComponent.GroundLayer) && isCanCling &&
                !PositionCheck.PlatformCheck(playerState.transform.TransformPoint(playerState.MovementComponent.WallCheckPosition), playerState.transform.right * playerState.transform.localScale.x, playerState.MovementComponent.WallRayLength,
                    playerState.MovementComponent.GroundLayer))
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
            if (PositionCheck.ObstacleCheck(playerState.transform.TransformPoint(playerState.MovementComponent.CeilingCheckPosition), playerState.transform.up, playerState.MovementComponent.CeilRayLength, playerState.MovementComponent.GroundLayer) 
                && isCanCling &&
                !PositionCheck.PlatformCheck(playerState.transform.TransformPoint(playerState.MovementComponent.CeilingCheckPosition), playerState.transform.up, playerState.MovementComponent.CeilRayLength, playerState.MovementComponent.GroundLayer))
            {
                playerState.StateMachine.ChangeState(playerState.OnCeilingState);
            }
        }

        public void TryAttackSwitching()
        {
            var colliders = playerState.AgentBoxDetection.OverlapBoxNonAlloc();
            if (colliders == 0)
                return;
            // playerState.AttackComponent.TargetCollider2Ds = playerState.AgentBoxDetection.Buffer;
            
            // var colliders = playerState.AgentBoxDetection.OverlapBox();
            // if(colliders.Length == 0)
            //     return;
            // playerState.AttackComponent.TargetCollider2Ds = colliders;

            playerState.StateMachine.ChangeState(playerState.AttackState);
        }
    }
}