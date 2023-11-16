using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Agent
{
    public class Health : MonoBehaviour
    {
        public UnityEvent OnDead;

        public void GetHit()
        {
            OnDead.Invoke();

            Destroy(gameObject);
        }
    }
}