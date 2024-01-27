using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Components
{
    public class LevelPartsDisabler : MonoBehaviour
    {
        [SerializeField] private float disableDistance;
        [SerializeField] private List<Transform> originalLevelPartsList;
        [SerializeField] private float invokeCD;
        
        private List<Transform> alreadyDisabledLevelParts = new List<Transform>();
     
        private void Start()
        {
            alreadyDisabledLevelParts.Add(transform);
            InvokeRepeating(nameof(TryDisableLevelParts), 0, invokeCD);
        }
        
        private void TryDisableLevelParts()
        {
            var newList = originalLevelPartsList.Except(alreadyDisabledLevelParts);
            
            foreach (var item in newList)
            {
                if (transform.position.x - disableDistance > item.position.x)
                {
                    item.gameObject.SetActive(false);
                    alreadyDisabledLevelParts.Add(item);
                }
            }
        }
    }
}