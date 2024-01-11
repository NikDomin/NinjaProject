using System;
using System.Threading;
using System.Threading.Tasks;
using NewObjectPool;
using Projectiles;
using UnityEngine;
using Utils;

namespace Agent.Enemy.EnemyAttack
{
    public class EnemyRangedAttack: EnemyAttack
    {
        [SerializeField] private Transform exclamationPoint;
        [SerializeField] private LayerMask playerLayer;
        [SerializeField] private LayerMask raycastLayers;
        [SerializeField] private ProjectileTrigger projectileObject;
        [SerializeField] private int poolPreloadCount = 1;

        private Rigidbody2D _rigidbody2D;
        private AgentBoxDetection checkPlayerDetection;
        private PoolMono<ProjectileTrigger> projectilePool;
        
        private EnemyAI enemyAi;
        private EnemyAnimationEventHandler exclamationEventHandler;

        private CancellationTokenSource tokenSource = null;
        private CancellationToken token;
        
        #region Mono

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            checkPlayerDetection = GetComponent<AgentBoxDetection>();
            enemyAi = GetComponent<EnemyAI>();
            exclamationEventHandler = exclamationPoint.gameObject.GetComponent<EnemyAnimationEventHandler>();

            projectilePool = new PoolMono<ProjectileTrigger>(projectileObject, poolPreloadCount);
            projectilePool.autoExpand = true;
        }

        private void Start()
        {
            exclamationEventHandler.OnEndExclamationPoint += StartAttackPlayer;
            enemyAi.EnemyEventHandler.OnEndAttack += EndAttack;
            enemyAi.EnemyEventHandler.OnAttack += AttackPlayer;
        }
        private void FixedUpdate()
        {
            DetectPlayer();
        }
        private void OnDestroy()
        {
            exclamationEventHandler.OnEndExclamationPoint -= StartAttackPlayer;
            enemyAi.EnemyEventHandler.OnEndAttack -= EndAttack;
            enemyAi.EnemyEventHandler.OnAttack -= AttackPlayer;
        }

        #endregion


        #region TryAttack

        protected override void DetectPlayer()
        {
            base.DetectPlayer();
            
            if(TryAttacking)
                return;
            if (Attacking)
                return;
            int colliderCount = checkPlayerDetection.OverlapBoxNonAlloc();
            if (colliderCount == 0)
                return;
            
            
            if(((1<<GetPlayerRaycast().collider.gameObject.layer) & playerLayer) != 0)
                TryAttack();
        }

        private void TryAttack()
        {
            exclamationPoint.gameObject.SetActive(true);
            TryAttacking = true;
            TryAttackLogic();
        }

        private async void TryAttackLogic()
        {
            tokenSource = new CancellationTokenSource();
            token = tokenSource.Token;
            try
            {
                while (true)
                {
                    await DelayCheckForPlayer(100, token);
                    if(!CheckForPlayerInBox())
                        DeniedAttack();
                    else if(((1<<GetPlayerRaycast().collider.gameObject.layer) & playerLayer) == 0)
                        DeniedAttack();
                    else if(GetPlayerRaycast().collider == null)
                        DeniedAttack();
                }
            }
            catch (Exception)
            {
                Debug.Log("Try attack logic was canceled");
            }

        }

        private void DeniedAttack()
        {
            tokenSource?.Cancel();
            tokenSource?.Dispose();
            tokenSource = null;
            TryAttacking = false;

            exclamationPoint.gameObject.SetActive(false);
        }

        private bool CheckForPlayerInBox()
        {
            int playerColliderCountCheck = checkPlayerDetection.OverlapBoxNonAlloc();
            
            if (playerColliderCountCheck == 0)
            {
                //DeniedAttack();
                return false;
            }

            return true;
        }

        private async Task DelayCheckForPlayer(int time, CancellationToken cancellationToken)
        {
            await Task.Delay(time, cancellationToken);
        }

        #endregion

        protected override void StartAttackPlayer()
        {
            Attacking = true;
            _rigidbody2D.velocity = new Vector2(0, 0);
            
            //Sprite Direction
            var playerCollider = GetPlayerCollider();
            if (playerCollider == null)
                return;
            AgentUtils.SpriteDirection(transform, playerCollider.transform.position - transform.position);

            exclamationPoint.gameObject.SetActive(false);

            tokenSource?.Cancel();
            tokenSource?.Dispose();
            tokenSource = null;

            enemyAi.EnemyAnimator.Anim.SetTrigger(enemyAi.EnemyAnimator.AttackTriggerKey);

        }
        
        private void AttackPlayer()
        {
            var playerCollider = GetPlayerCollider();
            if (playerCollider == null)
                return;
            
            ShootProjectile(projectilePool, transform, (playerCollider.transform.position - transform.position));
        }

        private void ShootProjectile(PoolMono<ProjectileTrigger> projectilePool, Transform objectTransform, Vector2 direction)
        {
            var projectile = projectilePool.GetFreeElement();

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);

            projectile.transform.rotation = targetRotation;
            projectile.transform.position = objectTransform.position + Vector3.right;

            var projectileMovement = projectile.GetComponent<ProjectileMovement>();
            projectileMovement.DirectionVector = direction.normalized;
        }

        private void EndAttack()
        {
            TryAttacking = false;
            Attacking = false;
            
             
            //Sprite Direction
            AgentUtils.SpriteDirection(transform, enemyAi.DirectionVector);

        }
        

        private RaycastHit2D GetPlayerRaycast()
        {
            Collider2D playerCollider = GetPlayerCollider(checkPlayerDetection.Buffer);
            if (playerCollider == null)
                return new RaycastHit2D();

            var playerPosition = playerCollider.transform.position;
            var enemyPosition = transform.position;
            Vector2 raycastDirection = new Vector2(
                    playerPosition.x - enemyPosition.x,
                    playerPosition.y - enemyPosition.y
                ).normalized;
            RaycastHit2D rayCastHit = Physics2D.Raycast(enemyPosition, raycastDirection,
                Vector3.Distance(playerPosition, enemyPosition), raycastLayers);
            return rayCastHit;
        }

        private Collider2D GetPlayerCollider()
        {
            int colliderCount = checkPlayerDetection.OverlapBoxNonAlloc();
            if (colliderCount == 0)
                return null;
            Collider2D playerCollider = GetPlayerCollider(checkPlayerDetection.Buffer);
            if (playerCollider == null)
                return null;
            return playerCollider;
        }
        private Collider2D GetPlayerCollider(Collider2D[] playerColliders)
        {
            foreach (var item in playerColliders)
            {
                if (item == null)
                    continue;
                return item;
            }

            return null;
        }
    }
}
