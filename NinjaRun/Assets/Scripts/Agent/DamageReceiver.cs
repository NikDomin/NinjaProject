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
        private Hittable hittable;

        private void Awake()
        {
            hittable = GetComponent<Hittable>();
        }


        public void Damage()
        {
            hittable.GetHit();
        }
    }
}