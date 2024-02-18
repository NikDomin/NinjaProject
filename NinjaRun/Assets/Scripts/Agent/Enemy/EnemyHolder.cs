using UnityEngine;
using Utils;

namespace Agent.Enemy
{
    public class EnemyHolder : MonoBehaviour
    {
        [SerializeField] protected Transform enemy;
        [SerializeField] protected Transform deadTransform;
        [SerializeField] protected EnemyAnimationEventHandler animationEventHandler;

        private void OnEnable()
        {
            animationEventHandler.OnDisableDeadBody += DisableDeadEnemy;
            enemy.gameObject.SetActive(true);
        }
        
        private void OnDisable()
        {
            animationEventHandler.OnDisableDeadBody -= DisableDeadEnemy;
        }

        public void EnableDeadEnemy()
        {
            if (enemy.TryGetComponent(out EnemyAI ai))
                AgentUtils.SpriteDirection(deadTransform, ai.DirectionVector);
            
            deadTransform.gameObject.SetActive(true);
            deadTransform.position = enemy.position;
        }

        private void DisableDeadEnemy()
        {
            deadTransform.gameObject.SetActive(false);
        }
    }
}