using System;
using Level.LevelSpawners;
using UnityEngine;

namespace Level
{
    // public interface ILevelPartSpawner
    // {
    //     public virtual void Spawn(int id){}
    // }
    public class LevelGenerator : MonoBehaviour
    {
        [SerializeField] private Transform player;
        [SerializeField] private float distanceSpawnLevelPart = 200f;
        [SerializeField] private float Xoffset;

        // private LevelPool levelPool;
        private Transform endTransform;
        private ILevelPartSpawner levelPartSpawner;

        private NewLevelPool newLevelPool;
        
        public int levelPartsCount { get; private set; }
        private void Awake()
        {
            // levelPool = GetComponent<LevelPool>();
            newLevelPool = GetComponent<NewLevelPool>();
            levelPartSpawner = GetComponent<ILevelPartSpawner>();
            
            
        }

        private void Start()
        {
            levelPartsCount = newLevelPool.GetLevelPartsCount();
            
            endTransform = transform;
        }

        private void FixedUpdate()
        {
            if(player==null)
                return;

            if (Vector3.Distance(player.position, endTransform.position) < distanceSpawnLevelPart)
            {
                // SpawnLevelPart(Random.Range(1, levelPartsCount));
                
                levelPartSpawner.Spawn(SpawnLevelPart);
            }
        }

        private void SpawnLevelPart(int id)
        {
            try
            {
                SpawnLevelPart(endTransform.position, id);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                throw;
            }
        }

        private void SpawnLevelPart(Vector3 spawnPosition, int id)
        {
            Vector3 offset = new Vector3(spawnPosition.x + Xoffset, spawnPosition.y, spawnPosition.z);
            
            LevelPart levelPart = newLevelPool.GetLevelPart(id);
            levelPart.transform.position = offset;

            endTransform = levelPart.EndTransform;

            // return gm.transform;
        }
    }
}