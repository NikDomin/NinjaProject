namespace Agent.Player.PlayerStateMachine
{
    public class BasedState
    {
        public string StateName;
        
        protected PlayerState player;
        protected Assets.Scripts.Agent.Player.PlayerStateMachine.PlayerStateMachine playerStateMachine;

        public BasedState(PlayerState player, Assets.Scripts.Agent.Player.PlayerStateMachine.PlayerStateMachine playerStateMachine)
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