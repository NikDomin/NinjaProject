using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace Components
{
    public class LevelPartsEnabler : MonoBehaviour
    {
        // [SerializeField] private Transform player;
        [SerializeField] private float enableDistance;
        [SerializeField] private List<Transform> originalLevelPartsList;
        [SerializeField] private float invokeCD;

        private List<Transform> alreadyActiveLevelParts = new List<Transform>();

        private void Awake()
        {
            foreach (var item in originalLevelPartsList)
                item.gameObject.SetActive(false);
        }

        private void Start()
        {
            alreadyActiveLevelParts.Add(transform);
            InvokeRepeating(nameof(TryEnableLevelParts), 0, invokeCD);
        }

        private void TryEnableLevelParts()
        {
            var newList = originalLevelPartsList.Except(alreadyActiveLevelParts);
            
            foreach (var item in newList)
            {
                if (Vector3.Distance(item.transform.position, transform.position) < enableDistance)
                {
                    item.gameObject.SetActive(true);
                    alreadyActiveLevelParts.Add(item);
                }
            }
        }
        
    }
}