using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Agent.Enemy
{
    public class EnemyAnimator : MonoBehaviour
    {
        public Animator Anim { get; private set; }

        public readonly int IsRunningKey = Animator.StringToHash("isRunning");
        public readonly int AttackTriggerKey = Animator.StringToHash("AttackTrigger");

        private void Awake()
        {
            Anim = GetComponent<Animator>();
        }
    }
}