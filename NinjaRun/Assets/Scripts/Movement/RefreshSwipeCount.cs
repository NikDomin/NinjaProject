using UnityEngine;

namespace Movement
{
    public class RefreshSwipeCount : MonoBehaviour
    {
        public void Refresh(Collider2D collider2D)
        {
            if (collider2D.TryGetComponent(out NewSwipeDetection swipeDetection))
            {
                swipeDetection.RefreshCurrentSwipeCount();
            }
        }
    }
}