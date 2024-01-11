using UnityEngine;

namespace Agent.Enemy.EnemyMovement
{
    public class EnemyFlyMovement: EnemyAbstractMovement
    {
        [SerializeField] private Transform firstPosition;
        [SerializeField] private Transform secondPosition;
        [SerializeField] private float flySpeed;
        
        private Vector2 directionVector;
        private Rigidbody2D _rigidbody2D;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            IsCanMove = true;
        }

        private void OnEnable()
        {
            ChangeDirection(firstPosition.position - transform.position);
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