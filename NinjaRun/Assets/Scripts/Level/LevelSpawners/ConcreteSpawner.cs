using UnityEngine;

namespace Level.LevelSpawners
{
    public delegate void LevelPartSpawnAction(int id);
    
    public class ConcreteSpawner: MonoBehaviour, ILevelPartSpawner
    {
        [SerializeField] private int idToSpawn;
        
        private LevelPartSpawnAction levelPartCallback;
        
        public void Spawn(LevelPartSpawnAction action)
        {
            action?.Invoke(idToSpawn);
        }
    }
}