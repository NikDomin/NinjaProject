using UnityEngine;

namespace Components
{
    public class DisableObjectComponent : MonoBehaviour
    {
        public void DisableGameObject()
        {
            gameObject.SetActive(false);
        }
    }
}