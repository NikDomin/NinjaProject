using System;
using Agent;
using Assets.Scripts.Agent;
using UnityEngine;

namespace Traps
{
    public class TrapCollider : MonoBehaviour
    {
        [SerializeField] private LayerMask playerLayer;
        
        private Collider2D collider2D;

        private void Awake()
        {
            collider2D = GetComponent<Collider2D>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collider2D.isTrigger)
                return;
            
            if ((playerLayer & (1 << collision.gameObject.layer)) != 0)
            {
                var collisionHealth = collision.gameObject.GetComponent<Health>();
                collisionHealth.GetHit();
            }
        }
        
        private void OnTriggerEnter2D(Collider2D item)
        {
            if (!collider2D.isTrigger)
                return;
            
            if ((playerLayer & (1 << item.gameObject.layer)) != 0)
            {
                var collisionHealth = item.gameObject.GetComponent<Health>();
                collisionHealth.GetHit();
            }
        }
    }
}