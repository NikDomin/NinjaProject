using System.Collections;
using Assets.Scripts.Agent;
using Assets.Scripts.Agent.Player;
using ObjectsPool;
using UnityEngine;
using Utils;

namespace Agent.Player
{
    public class PlayerAttack : AttackComponent
    {
        private PlayerAnimationHandler animationHandler;
        [SerializeField] private Transform AttackEffect;
        [SerializeField] private float effectTime;

        private AgentBoxDetection agentBoxDetection;
        private GameObjectPool attackEffectObjectPool;

        private GameObject effect;

        #region Mono

        private void Awake()
        {
            animationHandler = GetComponent<PlayerAnimationHandler>();
            agentBoxDetection = GetComponent<AgentBoxDetection>();
            attackEffectObjectPool = new GameObjectPool(AttackEffect.gameObject, 2);
        }

        private void OnEnable()
        {
            animationHandler.OnAttack += StartAttack;
            
        }

        private void OnDisable()
        {
            animationHandler.OnAttack -= StartAttack;
            // attackEffectObjectPool.ReturnAll();
            
        }
        
        #endregion

        #region Attack

        private void StartAttack()
        {
            TargetCollider2Ds = agentBoxDetection.OverlapBox();
            
            if (TargetCollider2Ds == null)
                return;
            if(TargetCollider2Ds.Length == 0)
                return;
            
            Attack(TargetCollider2Ds);
        }
        protected override void Attack(Collider2D[] targetCollider2Ds)
        {
            base.Attack(targetCollider2Ds);
            
            //AttackEffect.gameObject.SetActive(true);
            effect = attackEffectObjectPool.Get();
            effect.transform.position = transform.position;
            //effect scale
            AgentUtils.SpriteDirection(effect.transform, transform);

            StartCoroutine(DelayEndEffect());

            foreach (var item in targetCollider2Ds)
            {
                if (item.TryGetComponent(out IDamageable damageable))
                    damageable.Damage();
            }

            TargetCollider2Ds = null;
        }

        #endregion

        private IEnumerator DelayEndEffect()
        {
            yield return new WaitForSeconds(effectTime);
            attackEffectObjectPool.Return(effect);
        }
    }
}