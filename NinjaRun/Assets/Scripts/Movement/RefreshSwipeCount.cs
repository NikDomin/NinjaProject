using Level.Add;
using UnityEngine;

namespace Movement
{
    public class RefreshSwipeCount : MonoBehaviour, IWatchedAdd
    {
        public void SuccessWatchedAdd()
        {
            var player = FindObjectOfType<NewSwipeDetection>().gameObject;
            if(Vector3.Distance(player.transform.position, transform.position) < 200) 
                gameObject.SetActive(true);
        }

        public void Refresh(Collider2D collider2D)
        {
            if (collider2D.TryGetComponent(out NewSwipeDetection swipeDetection))
            {
                swipeDetection.RefreshCurrentSwipeCount();
            }
        }
    }
}