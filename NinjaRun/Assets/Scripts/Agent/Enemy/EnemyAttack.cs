using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Agent.Enemy
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