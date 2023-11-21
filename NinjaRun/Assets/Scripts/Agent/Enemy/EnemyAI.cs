using System.Collections;
using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.Agent.Enemy
{
    [RequireComponent(typeof(Animator), typeof(Rigidbody2D), typeof(EnemyAnimator))]
    public class EnemyAI : MonoBehaviour
    {
        [SerializeField] private Transform leftCorner;
        [SerializeField] private Transform rightCorner;
        [SerializeField] private float runSpeed;

        public Vector2 directionVector { get; private set; }

        public EnemyAnimator EnemyAnimator { get; private set; }
        public EnemyAnimationEventHandler EnemyEventHandler { get; private set; }
        
        public Rigidbody2D _rigidbody2D { get; private set; }
        private EnemyAttack enemyAttack; 

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            EnemyAnimator = GetComponent<EnemyAnimator>();
            EnemyEventHandler = GetComponent<EnemyAnimationEventHandler>();
            enemyAttack = GetComponent<EnemyAttack>();
        }

        private void Start()
        {
            directionVector = Vector2.left;
        }

        private void FixedUpdate()
        {

            if(transform.position.x > rightCorner.position.x)
                ChangeDirection(Vector2.left);
            if(transform.position.x<leftCorner.position.x)
                ChangeDirection(Vector2.right);

            //if (Vector2.Distance(transform.position, leftCorner.position) < 2f)
            //{
            //    ChangeDirection(Vector2.right);
            //}
            //if (Vector2.Distance(transform.position, rightCorner.position) < 2f)
            //{
            //    ChangeDirection(Vector2.left);
            //}

            if (enemyAttack.Attacking)
                return;

            _rigidbody2D.velocity = new Vector2(runSpeed * directionVector.x * Time.deltaTime, 0);
            AgentUtils.SpriteDirection(transform, directionVector);

            EnemyAnimator.Anim.SetBool(EnemyAnimator.IsRunningKey, true);
        }

        private void ChangeDirection(Vector2 newDirection)
        {
            directionVector = newDirection;
        }
        
    }
}