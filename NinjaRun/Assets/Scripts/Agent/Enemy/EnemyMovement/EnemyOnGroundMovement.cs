using Level.Resettable;
using Movement;
using UnityEngine;

namespace Agent.Enemy.EnemyMovement
{
    public enum EnemyMovementType
    {
        WithEnemyAi,
        Simple,
    }
    public class EnemyOnGroundMovement: EnemyAbstractMovement, IResettable
    {
        [SerializeField] private Transform leftCorner;
        [SerializeField] private Transform rightCorner;
        [SerializeField] private float runSpeed;
        [SerializeField] private float groundCheckRadius;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private Vector3 groundCheckPosition;
        [SerializeField] private EnemyMovementType enemyMovementType;
        
        private Vector2 directionVector;
        private Rigidbody2D _rigidbody;
        private EnemyAnimator enemyAnimator;
        private Vector3 resetPosition;
        
        #region Mono

        private void Awake()
        {
            enemyAnimator = GetComponent<EnemyAnimator>();
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void OnEnable()
        {
            resetPosition = transform.position;
            ChangeDirection(Vector2.right);
        }

        private void FixedUpdate()
        {
            //enemy on the ground?
            IsCanMove = PositionCheck.GroundCheck(transform.TransformPoint(groundCheckPosition), groundCheckRadius, groundLayer);
            if (!IsCanMove)
                enemyAnimator.Anim.SetBool(enemyAnimator.DropOutKey, true);
            else
            {
                enemyAnimator.Anim.SetBool(enemyAnimator.DropOutKey, false);
            }
            
            if(enemyMovementType == EnemyMovementType.Simple)
                Movement();
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.TransformPoint(groundCheckPosition), groundCheckRadius);
        }
        #endregion

        public void Reset()
        {
            transform.position = resetPosition;
            gameObject.SetActive(true);
            ChangeDirection(Vector2.right);
        }

        public override void Movement()
        {
            base.Movement();
            
            if(transform.position.x > rightCorner.position.x)
                ChangeDirection(Vector2.left);
            if(transform.position.x<leftCorner.position.x)
                ChangeDirection(Vector2.right);
            
            _rigidbody.velocity = new Vector2(runSpeed * directionVector.x * Time.deltaTime, -2);
        }
        
        private void ChangeDirection(Vector2 newDirection)
        {
            directionVector = newDirection;
            OnChangeDirectionVector?.Invoke(newDirection);
        }
        
        
    }
}