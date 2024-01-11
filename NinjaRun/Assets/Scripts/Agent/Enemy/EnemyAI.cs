using Agent.Enemy.EnemyMovement;
using UnityEngine;
using Utils;

namespace Agent.Enemy
{
    [RequireComponent(typeof(Animator), typeof(Rigidbody2D), typeof(EnemyAnimator))]
    public class EnemyAI : MonoBehaviour
    {
        public Vector2 DirectionVector { get; private set; }
        public EnemyAnimator EnemyAnimator { get; private set; }
        public EnemyAnimationEventHandler EnemyEventHandler { get; private set; }
        
        
        private EnemyAttack.EnemyAttack enemyAttack;
        private EnemyAbstractMovement enemyMovement;

        private void Awake()
        {
            EnemyAnimator = GetComponent<EnemyAnimator>();
            EnemyEventHandler = GetComponent<EnemyAnimationEventHandler>();
            enemyAttack = GetComponent<EnemyAttack.EnemyAttack>();
            enemyMovement = GetComponent<EnemyAbstractMovement>();
        }

        private void Start()
        {
            enemyMovement.OnChangeDirectionVector += ChangeDirectionVector;
        }

        private void OnDestroy()
        {
            enemyMovement.OnChangeDirectionVector -= ChangeDirectionVector;
        }

        private void FixedUpdate()
        {
            if (enemyAttack.Attacking)
                return;
            if (!enemyMovement.IsCanMove)
                return;
            
            enemyMovement.Movement();
            
            AgentUtils.SpriteDirection(transform, DirectionVector);

            EnemyAnimator.Anim.SetBool(EnemyAnimator.IsMoveKey, true);
        }

        private void ChangeDirectionVector(Vector2 direction)
        {
            DirectionVector = direction;
        }
    }
}