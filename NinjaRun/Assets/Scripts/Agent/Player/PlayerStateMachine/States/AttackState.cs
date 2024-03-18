namespace Agent.Player.PlayerStateMachine.States
{
    public class AttackState : BasedState, IFlyState
    {
        private PlayerState playerState;
        private Assets.Scripts.Agent.Player.PlayerStateMachine.PlayerStateMachine stateMachine;

        public AttackState(PlayerState player, Assets.Scripts.Agent.Player.PlayerStateMachine.PlayerStateMachine playerStateMachine) : base(player, playerStateMachine)
        {
            playerState = player;
            stateMachine = playerStateMachine;
        }

        public override void EnterState()
        {
            base.EnterState();
            StateName = "AttackState";

            playerState.PlayerAnimator.Anim.SetTrigger(playerState.PlayerAnimator.AttackTriggerKey);

            //time
            playerState.AnimationHandler.TestActionTrigger();
        }

        public override void ExitState()
        {
            base.ExitState();
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

        public void TryFlySwitching()
        {
            playerState.StateMachine.ChangeState(playerState.FlyState);
        }
    }
}