using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Agent
{
    public class Health : MonoBehaviour
    {
        public UnityEvent OnDead;

        public void Hit()
        {
            Destroy(gameObject);
            OnDead.Invoke();
        }
    }
}