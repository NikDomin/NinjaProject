using System.Collections;
using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.Agent.Enemy
{
    [RequireComponent(typeof(Animator), typeof(Rigidbody2D))]
    public class EnemyAI : MonoBehaviour
    {
        [SerializeField] private Transform leftCorner;
        [SerializeField] private Transform rightCorner;
        [SerializeField] private float runSpeed;

        public Vector2 directionVector { get; private set; }

        public EnemyAnimator EnemyAnimator { get; private set; }
        private Rigidbody2D _rigidbody2D;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            EnemyAnimator = GetComponent<EnemyAnimator>();
        }

        private void Start()
        {
            directionVector = Vector2.left;
        }

        private void FixedUpdate()
        {
            if (Vector2.Distance(transform.position, leftCorner.position) < 1f)
            {
                ChangeDirection(Vector2.right);
            }
            if (Vector2.Distance(transform.position, rightCorner.position) < 1f)
            {
                ChangeDirection(Vector2.left);
            }
                
            _rigidbody2D.velocity = new Vector2(runSpeed * directionVector.x * Time.deltaTime, 0);
            AgentUtils.SpriteDirection(transform, directionVector);
        }

        private void ChangeDirection(Vector2 newDirection)
        {
            directionVector = newDirection;
        }
        
    }
}