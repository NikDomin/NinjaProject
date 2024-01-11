using UnityEngine;

namespace Agent.Enemy
{
    public class EnemyAnimator : MonoBehaviour
    {
        public Animator Anim { get; private set; }

        public readonly int IsMoveKey = Animator.StringToHash("isMoving");
        public readonly int AttackTriggerKey = Animator.StringToHash("AttackTrigger");
        public readonly int DropOutKey = Animator.StringToHash("isDropOut");
        
        private void Awake()
        {
            Anim = GetComponent<Animator>();
        }
    }
}