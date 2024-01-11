using UnityEngine;
using UnityEngine.Events;

namespace Components
{
    public class EnterTriggerComponent : MonoBehaviour
    {
        [SerializeField] private LayerMask playerLayer;

        public UnityEvent OnTrigger;
        public UnityEvent<Collider2D> OnTriggerWithCollider;

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if ((playerLayer & (1 << collider.gameObject.layer)) != 0)
            {
                OnTrigger?.Invoke();
                OnTriggerWithCollider?.Invoke(collider);
            }
        }
            
    }
}