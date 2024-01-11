using UnityEngine;

namespace Agent.Enemy.EnemyAttack
{
    public abstract class EnemyAttack : MonoBehaviour
    {
        [HideInInspector] public bool Attacking;
        [HideInInspector] public bool TryAttacking;

        protected virtual void DetectPlayer()
        {

        }

        protected virtual void StartAttackPlayer()
        {

        }
    }
}