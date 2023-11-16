using System;
using System.Collections;
using Assets.Scripts.ObjectsPool;
using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.Agent.Player
{
    public class PlayerAttack : AttackComponent
    {
        private PlayerAnimationHandler animationHandler;
        [SerializeField] private Transform AttackEffect;
        [SerializeField] private int effectTime;

        private GameObjectPool attackEffectObjectPool;

        private void Awake()
        {
            animationHandler = GetComponent<PlayerAnimationHandler>();

            attackEffectObjectPool = new GameObjectPool(AttackEffect.gameObject, 2);
        }

        private void OnEnable()
        {
            animationHandler.OnAttack += StartAttack;
            
        }

        private void OnDisable()
        {
            animationHandler.OnAttack -= StartAttack;
            attackEffectObjectPool.ReturnAll();
            
        }

        private void StartAttack()
        {
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
            var effect = attackEffectObjectPool.Get();
            effect.transform.position = transform.position;

            GameUtils.Timer(OnEndEffect, effectTime);

            

            foreach (var item in targetCollider2Ds)
            {
                if (item.TryGetComponent(out IDamageable damageable))
                {
                    damageable.Damage();
                }
            }

            TargetCollider2Ds = null;

            void OnEndEffect() => attackEffectObjectPool.Return(effect);
        }

     
    }
}