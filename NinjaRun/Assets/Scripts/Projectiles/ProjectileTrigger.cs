using System;
using Agent;
using Assets.Scripts.Agent;
using NewObjectPool;
using ObjectsPool;
using UnityEngine;

namespace Projectiles
{
    public class ProjectileTrigger: MonoBehaviour
    {
        // [NonSerialized] public PoolMono<ProjectileTrigger> ProjectilePool;
        
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private LayerMask playerLevel;

        // private void OnDisable()
        // {
        //     ProjectilePool.Return(gameObject);
        // }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if ((groundLayer.value & (1 << other.gameObject.layer)) > 0)
            {
                gameObject.SetActive(false);
            }
            else if ((playerLevel.value & (1 << other.gameObject.layer)) > 0)
            {
                if (other.TryGetComponent(out Health health))
                {
                    health.GetHit(); 
                    gameObject.SetActive(false);
                }
            }
        }
    }
}