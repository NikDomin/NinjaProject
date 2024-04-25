using DataPersistence.Data.SerializableTypes;

namespace DataPersistence.Data
{
    [System.Serializable]
    public class GameData
    {
        public int CoinsCount;
        // public SpriteLibraryAsset HeroSpriteLibrary;
        public int HeroSpriteLibraryID;
        
        public SerializableDictionary<string, bool> LevelPassed;
        public string levelNeedToPass;
        public SerializableDictionary<int, bool> PurchasedSkins;
        public int PurchasedSkinsCount;
        
        //Settings
        public float MasterVolume;
        public float SFXVolume;
        public float MusicVolume;
        
        //Achievement
        public bool IsBeginnerRunnerAlreadyComplited;
        
        public GameData()
        { 
            CoinsCount = 0;
            HeroSpriteLibraryID = 0;
            LevelPassed = new SerializableDictionary<string, bool>();
            levelNeedToPass = "1";
            PurchasedSkins = new SerializableDictionary<int, bool>();
            PurchasedSkinsCount = 1;
            PurchasedSkins.Add(0, true); // add default skin
            //Settings
            MasterVolume = 1f;
            SFXVolume = 1f;
            MusicVolume = 1f;
            //Achivement
            IsBeginnerRunnerAlreadyComplited = false;
        }
    }
}