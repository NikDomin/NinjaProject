using Assets.Scripts.Agent;
using UnityEngine;

namespace Traps
{
    
    public class SpearTrap : MonoBehaviour
    {
        [SerializeField] private float invokeCD;
        private Animator animator;
        private Collider2D collider2D;

        private readonly int AttackKey = Animator.StringToHash("Attack");

        private void Awake()
        {
            animator = GetComponent<Animator>();
            collider2D = GetComponent<Collider2D>();

            collider2D.enabled = false;
            InvokeRepeating(nameof(StartAttack), invokeCD, invokeCD);
        }
        

        #region ColliderComponent

        private void EnableTrigger()
        {
            collider2D.enabled = true;
        }

        private void DisableTrigger()
        {
            collider2D.enabled = false;
        }

        #endregion

        #region Attack

        private void StartAttack()
        {
            animator.SetTrigger(AttackKey);
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            Attack(other);
        }

        private void Attack(Collider2D collider)
        {
            if (collider.TryGetComponent(out IDamageable damageable))
            {
                damageable.Damage();
            }
        }

        #endregion
    }
}