using System;
using UnityEngine;

namespace Agent.Enemy
{
    public enum EnemyMovementType
    {
        WithEnemyAi,
        Simple,
    }
    public class EnemyMovement: MonoBehaviour
    {
        [SerializeField] private Transform leftCorner;
        [SerializeField] private Transform rightCorner;
        [SerializeField] private float runSpeed;

        [SerializeField] private EnemyMovementType enemyMovementType;
        
        private Vector2 directionVector;
        private Rigidbody2D _rigidbody;
        
        public Action<Vector2> OnChangeDirectionVector;

        #region Mono

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            ChangeDirection(Vector2.right);
        }

        private void FixedUpdate()
        {
            if(enemyMovementType == EnemyMovementType.Simple)
                Movement();
        }

        #endregion

        public void Movement()
        {
            if(transform.position.x > rightCorner.position.x)
                ChangeDirection(Vector2.left);
            if(transform.position.x<leftCorner.position.x)
                ChangeDirection(Vector2.right);
            
            _rigidbody.velocity = new Vector2(runSpeed * directionVector.x * Time.deltaTime, 0);
        }
        
        private void ChangeDirection(Vector2 newDirection)
        {
            directionVector = newDirection;
            OnChangeDirectionVector?.Invoke(newDirection);
        }
    }
}