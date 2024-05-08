using UnityEngine;
using UnityEngine.EventSystems;

namespace Movement
{
    public class DontClickArea : MonoBehaviour, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log("name click object: " + name);
        }
    }
}