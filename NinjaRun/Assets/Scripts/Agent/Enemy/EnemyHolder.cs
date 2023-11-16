using System.Collections;
using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.Agent.Enemy
{
    public class EnemyHolder : MonoBehaviour
    {
        [SerializeField] private Transform Enemy;
        [SerializeField] private Transform DeadTransform;
        [SerializeField] private EnemyAnimationEventHandler animationEventHandler;

        private void OnEnable()
        {
            animationEventHandler.OnDisableDeadBody += DisableDeadEnemy;
        }

        private void OnDisable()
        {
            animationEventHandler.OnDisableDeadBody -= DisableDeadEnemy;
        }

        public void EnableDeadEnemy()
        {
            if (Enemy.TryGetComponent(out EnemyAI ai))
                AgentUtils.SpriteDirection(DeadTransform, ai.directionVector);
            
            DeadTransform.gameObject.SetActive(true);
            DeadTransform.position = Enemy.position;
        }

        public void DisableDeadEnemy()
        {
            DeadTransform.gameObject.SetActive(false);
        }
    }
}