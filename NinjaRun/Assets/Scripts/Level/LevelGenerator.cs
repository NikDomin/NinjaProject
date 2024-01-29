
using Assets.Scripts.Level;
using UnityEngine;

namespace Level
{
    public class LevelGenerator : MonoBehaviour
    {
        [SerializeField] private Transform player;
        [SerializeField] private float distanceSpawnLevelPart = 200f;
        [SerializeField] private float Xoffset;
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
            Vector3 offset = new Vector3(spawnPosition.x + Xoffset, spawnPosition.y, spawnPosition.z);
            
            var gm = levelPool.RequestLevelPart(id);
            gm.transform.position = offset;

            endTransform = gm.GetComponent<LevelPart>().EndTransform;

            return gm.transform;
        }
    }
}