using System;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using Agent;
using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.Agent.Enemy
{
    [RequireComponent(typeof(EnemyAI), typeof(AgentBoxDetection))]
    public class EnemyMeleeAttack : EnemyAttack
    {
        [SerializeField] private Transform exclamationPoint;
        [SerializeField] private int TimeToPrepareAttack = 1000;
        [SerializeField] private int moveAttackVelocity;
        
        [SerializeField] private AgentBoxDetection checkPlayerDetection;
        [SerializeField] private AgentBoxDetection AttackPlayerDetection;

        private EnemyAI enemyAi;
        private EnemyAnimationEventHandler exclamationEventHandler;

        private CancellationTokenSource tokenSource = null;
        private CancellationToken token;

        private Collider2D[] playerCollider2Ds;
        private Collider2D[] checkForPlayer;

        #region Mono

        private void Awake()
        {
            //checkPlayerDetection = GetComponent<AgentBoxDetection>();
            enemyAi = GetComponent<EnemyAI>();
            exclamationEventHandler = exclamationPoint.gameObject.GetComponent<EnemyAnimationEventHandler>();
        }

        private void Start()
        {
            exclamationEventHandler.OnEndExclamationPoint += StartAttackPlayer;
            enemyAi.EnemyEventHandler.OnEndAttack += EndAttack;
            enemyAi.EnemyEventHandler.OnMoveAttack += MoveAttack;
            enemyAi.EnemyEventHandler.OnAttack += AttackPlayer;

            InvokeRepeating(nameof(DetectPlayer), 1f, 0.1f);
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

            int colliderCount = checkPlayerDetection.OverlapBoxNonAlloc(checkPlayerDetection.Buffer);
            
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
            int playerColliderCountCheck = checkPlayerDetection.OverlapBoxNonAlloc(checkPlayerDetection.Buffer);
            
            if (checkForPlayer.Length == 0)
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
            enemyAi._rigidbody2D.velocity = enemyAi.directionVector * moveAttackVelocity;
        }
    }
}