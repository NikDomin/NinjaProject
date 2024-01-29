using Level;
using UnityEngine;

namespace Components
{
    public class ObjectDisabler : MonoBehaviour
    {
        [SerializeField] private float disableDistance;
        [SerializeField] private float invokeCD;
        
        private void Start()
        {
            InvokeRepeating(nameof(TryDisableLevelParts), 0, invokeCD);
        }

        private void TryDisableLevelParts()
        {
            if (transform.position.x + 30f < DeathWall.deathWall.transform.position.x)
                gameObject.SetActive(false);
        }
    }
}