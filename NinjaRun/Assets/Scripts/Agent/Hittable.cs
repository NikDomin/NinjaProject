using UnityEngine;
using UnityEngine.Events;

namespace Agent
{
    public abstract class Hittable : MonoBehaviour
    {
        public UnityEvent OnDead;
        public abstract void GetHit();
        
    }
}