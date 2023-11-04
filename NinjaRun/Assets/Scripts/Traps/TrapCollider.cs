using System.Collections;
using Assets.Scripts.Agent;
using UnityEngine;

namespace Assets.Scripts.Traps
{
    public class TrapCollider : MonoBehaviour
    {
        [SerializeField] private LayerMask playerLayer;
        private void OnCollisionEnter2D(Collision2D collision)
        {
            Debug.Log(collision.gameObject.layer);
            //if (collision.gameObject.layer == playerLayer)
            //{
            //    var collisionHealth = collision.gameObject.GetComponent<Health>();
            //    collisionHealth.Hit();
            //}

            if ((playerLayer & (1 << collision.gameObject.layer)) != 0)
            {
                var collisionHealth = collision.gameObject.GetComponent<Health>();
                collisionHealth.Hit();
            }
        }
    }
}