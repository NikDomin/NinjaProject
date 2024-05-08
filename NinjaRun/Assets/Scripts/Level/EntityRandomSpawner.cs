using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Level
{
    public class EntityRandomSpawner: MonoBehaviour
    {
        [SerializeField] private Spawnable[] spawnables;
        
        private void OnEnable()
        {
            SpawnRandomObject();
        }
        
        private void SpawnRandomObject()
        {
            float totalWeight = 0f;
            
            foreach (Spawnable spawnable in spawnables)
            {
                totalWeight += spawnable.spawnWeight; 
                spawnable.prefab.SetActive(false);
            }

            int randomValue = Random.Range(0, Convert.ToInt32(totalWeight));
            float cumulativeWeight = 0f;

            foreach (Spawnable spawnable in spawnables)
            {
                cumulativeWeight += spawnable.spawnWeight;

                if (randomValue <= cumulativeWeight)
                {
                    // Instantiate(spawnable.prefab, transform.position, Quaternion.identity);
                    spawnable.prefab.SetActive(true);
                    return;
                }
            }
        }
    }
    
    [System.Serializable]
    public class Spawnable
    {
        public GameObject prefab;
        public float spawnWeight = 1;
    }
}