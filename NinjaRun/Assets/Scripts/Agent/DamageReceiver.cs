using System.Collections;
using Agent;
using UnityEngine;

namespace Assets.Scripts.Agent
{
    public interface IDamageable
    {
        void Damage();
    }

    public class DamageReceiver : MonoBehaviour, IDamageable
    {
        private Health health;

        private void Awake()
        {
            health = GetComponent<Health>();
        }


        public void Damage()
        {
            health.GetHit();
        }
    }
}