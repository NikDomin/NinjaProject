using Movement;
using UnityEngine;
using Achievement = Services.Achievement;

namespace Agent.Enemy
{
    public class EnemyDeath : MonoBehaviour
    {
        [field:SerializeField] public bool IsRefreshSwipeCount { get; private set; }
        private EnemyHealth enemyHealth;

        #region Mono

        private void Awake()
        {
            enemyHealth = GetComponent<EnemyHealth>();
        }

        private void OnEnable()
        {
            enemyHealth.OnDead.AddListener(IncreaseSlayerAchievement);
        }


        private void OnDisable()
        {
            enemyHealth.OnDead.RemoveListener(IncreaseSlayerAchievement);
        }

        #endregion

        public void PlayerRefreshSwipeCount(GameObject player)
        {
            if(player.TryGetComponent( out NewSwipeDetection swipeDetection))
            {
                swipeDetection.RefreshCurrentSwipeCount();
            }
        }
        private void IncreaseSlayerAchievement()
        {
            Achievement.Instance.Slayer();
        }
        
    }
}