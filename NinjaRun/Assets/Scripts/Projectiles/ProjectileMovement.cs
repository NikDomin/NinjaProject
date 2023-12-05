using System;
using UnityEngine;

namespace Projectiles
{
    [RequireComponent(typeof(Rigidbody2D), typeof(ProjectileTrigger))]
    public class ProjectileMovement: MonoBehaviour
    {
        [NonSerialized]public Vector3 DirectionVector;
        [SerializeField] private float speed;
        
        private Rigidbody2D _rigidbody2D;
        
        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            _rigidbody2D.velocity = new Vector2(speed * DirectionVector.x * Time.deltaTime,
                speed * DirectionVector.y * Time.deltaTime);
        }
    }
}