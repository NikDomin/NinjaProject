using System.Collections;
using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.Agent.Enemy
{
    [RequireComponent(typeof(EnemyAI), typeof(AgentBoxDetection))]
    public class EnemyMeleeAttack : EnemyAttack
    {
        [SerializeField] private Transform exclamationPoint;
        [SerializeField] private int TimeToPrepareAttack = 1000;
        
        private AgentBoxDetection boxDetection;
        private EnemyAI enemyAi;
        private EnemyAnimationEventHandler exclamationEventHandler;

        private Collider2D[] playerCollider2Ds;

        private void Awake()
        {
            boxDetection = GetComponent<AgentBoxDetection>();
            enemyAi = GetComponent<EnemyAI>();
            exclamationEventHandler = exclamationPoint.gameObject.GetComponent<EnemyAnimationEventHandler>();
        }

        private void OnEnable()
        {
            exclamationEventHandler.OnEndExclamationPoint += AttackPlayer;
        }
        private void OnDisable()
        {
            exclamationEventHandler.OnEndExclamationPoint -= AttackPlayer;
        }

        private void Start()
        {
            InvokeRepeating("DetectPlayer", 1f, 0.1f);
        }

        protected override void DetectPlayer()
        {
            base.DetectPlayer();

            playerCollider2Ds = boxDetection.OverlapBox();
            if(playerCollider2Ds == null)
                return;
            if (playerCollider2Ds.Length == 0)
                return;

            TryAttack();
        }

        private void TryAttack()
        {
            exclamationPoint.gameObject.SetActive(true);
        }

       

        protected override void AttackPlayer()
        {
            base.AttackPlayer();
            exclamationPoint.gameObject.SetActive(false);

            enemyAi.EnemyAnimator.Anim.SetTrigger(enemyAi.EnemyAnimator.AttackTriggerKey);

        }
    }
}