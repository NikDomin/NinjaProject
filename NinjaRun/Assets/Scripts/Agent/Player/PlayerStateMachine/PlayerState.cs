using Assets.Scripts.Agent.Player.PlayerStateMachine.States;
using Assets.Scripts.Movement;
using UnityEngine;

namespace Assets.Scripts.Agent.Player.PlayerStateMachine
{
    public class PlayerState : MonoBehaviour
    {
        public NewSwipeDetection SwipeDetection { get; private set; }
        public MovementComponent MovementComponent { get; private set; }

        public PlayerStateMachine StateMachine;
        public RunState RunState { get; set; }
        public JumpState JumpState { get; set; }
        public OnWallState OnWallState { get; set; }
        public FlyState FlyState { get; set; }

        private void Awake()
        {
            SwipeDetection = GetComponent<NewSwipeDetection>();
            MovementComponent = GetComponent<MovementComponent>();

            StateMachine= new PlayerStateMachine();

            RunState = new RunState(this, StateMachine);
            JumpState = new JumpState(this, StateMachine);
            OnWallState = new OnWallState(this, StateMachine);
            FlyState = new FlyState(this, StateMachine);
        }


        private void Start()
        {
            StateMachine.Initialize(RunState);
        }

        private void Update()
        {
            StateMachine.CurrentState.FrameUpdate();
        }
        private void FixedUpdate()
        {
            StateMachine.CurrentState.PhysicUpdate();
        }
    }
}