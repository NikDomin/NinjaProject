using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Agent.Player
{
    public class PlayerAnimator : MonoBehaviour
    {
        public Animator Anim { get; private set; }

        public readonly int IsRunningKey = Animator.StringToHash("isRunning");
        public readonly int StartJumpKey = Animator.StringToHash("startJumping");
        public readonly int OnWallTriggerKey = Animator.StringToHash("onWallTrigger");
        public readonly int EndWallTriggerKey = Animator.StringToHash("endWallTrigger");
        public readonly int OnCeilTriggerKey = Animator.StringToHash("OnCeilTrigger");
        public readonly int EndCeilTriggerKey = Animator.StringToHash("endCeilTrigger");
        public readonly int AttackTriggerKey = Animator.StringToHash("AttackTrigger ");

        private void Awake()
        {
            Anim = GetComponent<Animator>();
        }
     
    }
}