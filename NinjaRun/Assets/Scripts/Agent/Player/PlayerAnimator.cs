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
        

        private void Awake()
        {
            Anim = GetComponent<Animator>();
        }
     
    }
}