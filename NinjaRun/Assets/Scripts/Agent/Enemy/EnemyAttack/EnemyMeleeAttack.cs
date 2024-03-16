using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Agent.Enemy.EnemyMovement;
using Assets.Scripts.Agent;
using UnityEngine;

namespace Agent.Enemy.EnemyAttack
{
    [RequireComponent(typeof(EnemyAI), typeof(AgentBoxDetection))]
    public class EnemyMeleeAttack : EnemyAttack
    {
        [SerializeField] private Transform exclamationPoint;
        [SerializeField] private int moveAttackVelocity;
        
        [SerializeField] private AgentBoxDetection checkPlayerDetection;
        [SerializeField] private AgentBoxDetection AttackPlayerDetection;


        private Rigidbody2D rigidbody2D;
        
        private EnemyAI enemyAi;
        private EnemyAnimationEventHandler exclamationEventHandler;
        [SerializeField] private EnemyAbstractMovement enemyMovement;
        
        private CancellationTokenSource tokenSource = null;
        private CancellationToken token;
        

        #region Mono

        private void Awake()
        {
            //checkPlayerDetection = GetComponent<AgentBoxDetection>();
            rigidbody2D = GetComponent<Rigidbody2D>();
            enemyAi = GetComponent<EnemyAI>();
            exclamationEventHandler = exclamationPoint.gameObject.GetComponent<EnemyAnimationEventHandler>();
        }

        private void Start()
        {
            exclamationEventHandler.OnEndExclamationPoint += StartAttackPlayer;
            enemyAi.EnemyEventHandler.OnEndAttack += EndAttack;
            enemyAi.EnemyEventHandler.OnMoveAttack += MoveAttack;
            enemyAi.EnemyEventHandler.OnAttack += AttackPlayer;

            // InvokeRepeating(nameof(DetectPlayer), 1f, 0.1f);
        }

        private void FixedUpdate()
        {
            DetectPlayer();

            if (Attacking && !enemyMovement.IsCanMove)
            {
                StartCoroutine(DelayZeroVelocity());
            }
        }

        private IEnumerator DelayZeroVelocity()
        {
            yield return new WaitForSeconds(0.2f);
            rigidbody2D.velocity = Vector2.zero;
            rigidbody2D.velocity = Vector2.down;
        }

        private void OnDestroy()
        {
            exclamationEventHandler.OnEndExclamationPoint -= StartAttackPlayer;
            enemyAi.EnemyEventHandler.OnEndAttack -= EndAttack;
            enemyAi.EnemyEventHandler.OnMoveAttack -= MoveAttack;
            enemyAi.EnemyEventHandler.OnAttack -= AttackPlayer;
        }

        #endregion

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
            
            TryAttack();
        }

        #region TryAttack

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
                }
            }
            catch (Exception)
            {
                Debug.Log("Try attack logic was canceled");
            }

        }
        private async Task DelayCheckForPlayer(int time, CancellationToken _token)
        {
            await Task.Delay(time, _token);
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

        #endregion
       

        protected override void StartAttackPlayer()
        {
            base.StartAttackPlayer();
            Attacking = true;

            exclamationPoint.gameObject.SetActive(false);

            tokenSource?.Cancel();
            tokenSource?.Dispose();
            tokenSource = null;

            enemyAi.EnemyAnimator.Anim.SetTrigger(enemyAi.EnemyAnimator.AttackTriggerKey);

        }

        private void AttackPlayer()
        {
           var box = AttackPlayerDetection.OverlapBox();
           if (box == null)
               return;
           if (box.Length == 0)
               return;

           foreach (var item in box)
           {
                if (item.TryGetComponent(out IDamageable damageable))
                {
                    damageable.Damage();
                }
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

        private void EndAttack()
        {
            TryAttacking = false;
            Attacking = false;
        }

        private void MoveAttack()
        {
            rigidbody2D.velocity = enemyAi.DirectionVector * moveAttackVelocity;
        }
    }
}