using System;
using Level.Resettable;
using UnityEngine;

namespace Agent.Enemy.EnemyMovement
{
    public class SimpleGroundMovement : MonoBehaviour, IResettable
    {
        [SerializeField] private Transform leftCorner;
        [SerializeField] private Transform rightCorner;
        [SerializeField] private float runSpeed;
        
        private Vector2 directionVector;
        private Rigidbody2D _rigidbody;
        private Vector3 resetPosition;

        #region Mono

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void OnEnable()
        {
            resetPosition = transform.position;
        }

        private void Start()
        {
            ChangeDirection(Vector2.right);
        }
        
        private void FixedUpdate()
        {
            Movement();
        }

        #endregion

        public void Reset()
        {
            transform.position = resetPosition;
            ChangeDirection(Vector2.right);
            gameObject.SetActive(true);
        }

        private void Movement()
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
        }
    }
}