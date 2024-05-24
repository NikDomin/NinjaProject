using UnityEngine;

namespace Level.Resettable
{
    public class Disayable : MonoBehaviour, IResettable 
    {
        public void Reset()
        {
            gameObject.SetActive(false);
        }
    }
}