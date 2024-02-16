using UnityEngine;
using Random = UnityEngine.Random;


namespace Level.LevelSpawners
{
    public class RandomSpawner: MonoBehaviour, ILevelPartSpawner
    {
        private LevelPartSpawnAction levelPartCallback;

        private LevelGenerator levelGenerator;

        private void Awake()
        {
            levelGenerator = GetComponent<LevelGenerator>();
        }

        public void Spawn(LevelPartSpawnAction action)
        {
            var randomId = Random.Range(1, levelGenerator.levelPartsCount);
            action?.Invoke(randomId);
        }
    }
} 
