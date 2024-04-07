using Level;
using UnityEngine;
using UnityEngine.Events;

namespace Agent
{
    public class Health : MonoBehaviour
    {
        public UnityEvent OnDead;
        
        public void GetHit()
        {
            OnDead.Invoke();
            EffectsHandler.Instance.EnableHitParticle(transform.position);
            
            gameObject.SetActive(false);
        }

    }
}