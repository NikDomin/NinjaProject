using UnityEngine;

namespace Level
{
    public class EntityRandomSpawner: MonoBehaviour
    {
        private Spawnable[] spawnables;
        
        private void OnEnable()
        {
            SpawnRandomObject();
        }
        
        void SpawnRandomObject()
        {
            float totalWeight = 0f;

            foreach (Spawnable spawnable in spawnables)
            {
                totalWeight += spawnable.spawnWeight;
            }

            float randomValue = Random.Range(0f, totalWeight);
            float cumulativeWeight = 0f;

            foreach (Spawnable spawnable in spawnables)
            {
                cumulativeWeight += spawnable.spawnWeight;

                if (randomValue <= cumulativeWeight)
                {
                    Instantiate(spawnable.prefab, transform.position, Quaternion.identity);
                    break;
                }
            }
        }
    }
    
    [System.Serializable]
    public class Spawnable
    {
        public GameObject prefab;
        public float spawnWeight;
    }
}