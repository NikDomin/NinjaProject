using DataPersistence.Data.SerializableTypes;
using UnityEngine.U2D.Animation;

namespace DataPersistence.Data
{
    [System.Serializable]
    public class GameData
    {
        public int CoinsCount;
        // public SpriteLibraryAsset HeroSpriteLibrary;
        public int HeroSpriteLibraryID;
        
        public SerializableDictionary<int, bool> LevelPassed;
        public SerializableDictionary<int, bool> PurchasedSkins;
        
        public GameData()
        { 
            CoinsCount = 0;
            HeroSpriteLibraryID = 0;
            LevelPassed = new SerializableDictionary<int, bool>();
            PurchasedSkins = new SerializableDictionary<int, bool>();
            PurchasedSkins.Add(0, true); // add default skin
            
        }
    }
}