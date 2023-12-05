using System;
using Assets.Scripts.Agent;
using Assets.Scripts.ObjectsPool;
using UnityEngine;

namespace Projectiles
{
    public class ProjectileTrigger: MonoBehaviour
    {
        [NonSerialized] public GameObjectPool ProjectilePool;
        
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private LayerMask playerLevel;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if ((groundLayer.value & (1 << other.gameObject.layer)) > 0)
            {
                ProjectilePool.Return(gameObject);
            }
            else if ((playerLevel.value & (1 << other.gameObject.layer)) > 0)
            {
                if (other.TryGetComponent(out Health health))
                {
                    health.GetHit();
                    ProjectilePool.Return(gameObject);
                }
            }
        }
    }
}