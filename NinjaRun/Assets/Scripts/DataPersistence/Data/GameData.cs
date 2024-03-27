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
        
        public SerializableDictionary<string, bool> LevelPassed;
        public SerializableDictionary<int, bool> PurchasedSkins;
        
        //Settings
        public float MasterVolume;
        public float SFXVolume;
        public float MusicVolume;
        
        
        public GameData()
        { 
            CoinsCount = 0;
            HeroSpriteLibraryID = 0;
            LevelPassed = new SerializableDictionary<string, bool>();
            PurchasedSkins = new SerializableDictionary<int, bool>();
            PurchasedSkins.Add(0, true); // add default skin
            //Settings
            MasterVolume = 1f;
            SFXVolume = 1f;
            MusicVolume = 1f;
        }
    }
}