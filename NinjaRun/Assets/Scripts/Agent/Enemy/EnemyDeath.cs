using Movement;
using UnityEngine;

namespace Agent.Enemy
{
    public class EnemyDeath : MonoBehaviour
    {
        public void PlayerRefreshSwipeCount(GameObject player)
        {
            if(player.TryGetComponent( out NewSwipeDetection swipeDetection))
            {
                swipeDetection.RefreshCurrentSwipeCount();
            }
        }
    }
}