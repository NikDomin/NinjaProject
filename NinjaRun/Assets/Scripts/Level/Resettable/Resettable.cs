using UnityEngine;

namespace Level.Resettable
{
    public class Resettable : MonoBehaviour, IResettable
    {
        
        private Vector3 resetPosition;
        private void OnEnable()
        {
            resetPosition = transform.position;
        }

        public void Reset()
        {
            transform.position = resetPosition;
            gameObject.SetActive(true);
        }
    }
}