﻿using System;
using Agent.Player.PlayerStateMachine.States;
using Assets.Scripts.Agent;
using Level.Resettable;
using Movement;
using UnityEngine;

namespace Agent.Player.PlayerStateMachine
{
    public class PlayerState : MonoBehaviour, IResettable
    {
        #region Events

        public event Action OnLanding;
        public event Action OnLevelReset;

        #endregion
        #region PublicFields

        public NewSwipeDetection SwipeDetection { get; private set; }
        public MovementComponent MovementComponent { get; private set; }
        public PlayerAnimator PlayerAnimator { get; private set; }
        public SpriteRenderer SpriteRenderer { get; private set; }
        public AgentBoxDetection AgentBoxDetection { get; private set; }
        public AttackComponent AttackComponent { get; private set; } 
        //time
        public PlayerAnimationHandler AnimationHandler { get; private set; }
        

        #endregion
        #region States

        public Assets.Scripts.Agent.Player.PlayerStateMachine.PlayerStateMachine StateMachine;
        public RunState RunState { get; set; }
        public JumpState JumpState { get; set; }
        public OnWallState OnWallState { get; set; }
        public OnCeilingState OnCeilingState { get; set; }
        public FlyState FlyState { get; set; }
        public AttackState AttackState { get; set; }
        

        #endregion
        
        #region PrivateFields

        [SerializeField] private string currentStateName;

        #endregion

        #region Mono

        private void Awake()
        {
            SwipeDetection = GetComponent<NewSwipeDetection>();
            MovementComponent = GetComponent<MovementComponent>();
            PlayerAnimator = GetComponent<PlayerAnimator>();
            SpriteRenderer = GetComponent<SpriteRenderer>();
            AgentBoxDetection = GetComponent<AgentBoxDetection>();
            AttackComponent = GetComponent<AttackComponent>();
            AnimationHandler = GetComponent<PlayerAnimationHandler>();

            StateMachine= new Assets.Scripts.Agent.Player.PlayerStateMachine.PlayerStateMachine();

            RunState = new RunState(this, StateMachine);
            JumpState = new JumpState(this, StateMachine);
            OnWallState = new OnWallState(this, StateMachine);
            OnCeilingState = new OnCeilingState(this, StateMachine);
            FlyState = new FlyState(this, StateMachine);
            AttackState = new AttackState(this, StateMachine);
        }


        private void Start()
        {
            StateMachine.Initialize(RunState);
        }

        private void Update()
        {
            StateMachine.CurrentState.FrameUpdate();
            
            //ForDebug
            currentStateName = StateMachine.CurrentState.StateName;
        }
        private void FixedUpdate()
        {
            StateMachine.CurrentState.PhysicUpdate();
        }

        #endregion

        public void LandingTrigger()
        {
            OnLanding?.Invoke();
        }

        public void Reset()
        {
            OnLevelReset?.Invoke();
        }
    }
}