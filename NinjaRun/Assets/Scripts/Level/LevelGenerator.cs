using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Level
{
    public class LevelGenerator : MonoBehaviour
    {
        [SerializeField] private Transform player;
        [SerializeField] private float distanceSpawnLevelPart = 200f;
        [SerializeField] private int IdToSpawn;

        private LevelPool levelPool;
        private Transform endTransform;

        private void Awake()
        {
            levelPool = GetComponent<LevelPool>();
        }

        private void Start()
        {
            endTransform = transform;
        }

        private void FixedUpdate()
        {
            if(player==null)
                return;

            if (Vector3.Distance(player.position, endTransform.position) < distanceSpawnLevelPart)
            {
                SpawnLevelPart(IdToSpawn);
            }
        }

        private void SpawnLevelPart(int id)
        {
            Transform lastLevelPartTransform = SpawnLevelPart(endTransform.position, id);
            
        }

        private Transform SpawnLevelPart(Vector3 spawnPosition, int id)
        {
            var gm = levelPool.RequestLevelPart(id);
            gm.transform.position = spawnPosition;

            endTransform = gm.GetComponent<LevelPart>().EndTransform;

            return gm.transform;
        }
    }
}