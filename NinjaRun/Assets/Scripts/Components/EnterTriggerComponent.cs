using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Components
{
    public class EnterTriggerComponent : MonoBehaviour
    {
        [SerializeField] private LayerMask playerLayer;

        public UnityEvent OnTrigger;

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if ((playerLayer & (1 << collider.gameObject.layer)) != 0)
                OnTrigger.Invoke();
        }
            
    }
}