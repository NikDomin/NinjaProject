using System.Collections.Generic;
using DataPersistence.Data.SerializableTypes;

namespace DataPersistence.Data
{
    [System.Serializable]
    public class GameData
    {
        public int CoinsCount;

        public SerializableDictionary<int, bool> LevelPassed;

        public GameData()
        {
            this.CoinsCount = 0;
            LevelPassed = new SerializableDictionary<int, bool>();
        }
    }
}