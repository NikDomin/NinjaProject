using UnityEngine;

namespace Level.Resettable
{
    public class Destoyable : MonoBehaviour, IResettable
    {
        public void Reset()
        {
            Destroy(gameObject);
        }
    }
}