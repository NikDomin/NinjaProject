using System;
using NewObjectPool;
using UnityEngine;

namespace Level
{
    public class NewLevelPool: MonoBehaviour
    {
        [SerializeField] private NewLevelPartSlot[] levelPartSlots;

        private void Awake()
        {
            foreach (var item in levelPartSlots)
            {
                item.levelPartPool = new PoolMono<LevelPart>(item.levelPart, item.poolPreloadCount, transform);
                item.levelPartPool.autoExpand = true;
            }
        }

        public LevelPart GetLevelPart(int id)
        {
            NewLevelPartSlot slot = Array.Find(levelPartSlots, element => element.levelPart.ID == id);
            if (slot == null)
            {
                Debug.LogError("Part " + id + "not found");
                return null;
            }

            return slot.levelPartPool.GetFreeElement();
        }
        
        public int GetLevelPartsCount()
        {
            int count = 0;
            foreach (var item in levelPartSlots)
            {
                count++;
            }

            return count;
        }
        public void ResetSlots()
        {
            foreach (var item in levelPartSlots)
            {
                item.levelPartPool = new PoolMono<LevelPart>(item.levelPart, item.poolPreloadCount, transform);
                item.levelPartPool.autoExpand = true;
            }
        }
    }

    [Serializable]
    public class NewLevelPartSlot
    {
        public LevelPart levelPart;
        public PoolMono<LevelPart> levelPartPool;
        [field: SerializeField] public int poolPreloadCount { get; private set; } = 2;
    }
}