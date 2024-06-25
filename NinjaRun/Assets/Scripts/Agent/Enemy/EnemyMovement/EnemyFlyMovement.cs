using System;
using Level.Resettable;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Agent.Enemy.EnemyMovement
{
    public class EnemyFlyMovement: EnemyAbstractMovement, IResettable
    {
        [SerializeField] private Transform firstPosition;
        [SerializeField] private Transform secondPosition;
        [SerializeField] private float flySpeed;
        
        private Vector2 directionVector;
        private Rigidbody2D _rigidbody2D;
        private Vector3 resetPosition;
        
        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            IsCanMove = true;
        }
        
        private void OnEnable()
        {
            resetPosition = transform.position;
            ChangeDirection((firstPosition.position - transform.position).normalized);
        }
        

        public void Reset()
        {
            transform.position = resetPosition;
            gameObject.SetActive(true);
            ChangeDirection((firstPosition.position - transform.position).normalized);
        }

        public override void Movement()
        {
            base.Movement();
            if (Vector2.Distance(transform.position, firstPosition.position) < 1f)
                ChangeDirection((secondPosition.position - transform.position).normalized);
            else if (Vector2.Distance(transform.position, secondPosition.position) < 1f)
                ChangeDirection((firstPosition.position - transform.position).normalized);

            _rigidbody2D.velocity = new Vector2(flySpeed * directionVector.x * Time.deltaTime,
                flySpeed * directionVector.y * Time.deltaTime);
        }

        private void ChangeDirection(Vector2 newDirection)
        {
            directionVector = newDirection;
            OnChangeDirectionVector?.Invoke(newDirection);
        }
    }
}