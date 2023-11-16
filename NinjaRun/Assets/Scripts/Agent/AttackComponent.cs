using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Agent
{
    public abstract class AttackComponent : MonoBehaviour
    {
        public Collider2D[] TargetCollider2Ds;

        protected virtual void Attack(Collider2D[] targetCollider2Ds)
        {

        }
    }
}