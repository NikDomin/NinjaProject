using System;
using UI;
using UnityEngine;
using Random = System.Random;

namespace DataPersistence.Data
{
    public class TestHandler : MonoBehaviour
    {
        public static TestHandler Instance;
        private GameData gameDataToLoad;

        private GameData mainGameData;

        private GameData settingsGameData;

        public GameData TestMainGameData;
        public CloudSaveGameUI CloudSaveGameUI;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            settingsGameData = new GameData();
            settingsGameData.CoinsCount = 250;
            settingsGameData.HeroSpriteLibraryID = 3;
        }

        private void Start()
        {
            CloudSaveGameUI = UI.CloudSaveGameUI.Instance;
        }

        private void OpenSave()
        {
            LoadCallback();
        }

        private void LoadCallback()
        {
            gameDataToLoad = settingsGameData;
            // Random random = new Random();
            // gameDataToLoad.CoinsCount = random.Next(100, 200);
            // gameDataToLoad.HeroSpriteLibraryID = random.Next(0, 14);
            mainGameData = gameDataToLoad;
        }
        
        private void Load()
        {
            gameDataToLoad = new GameData();
            OpenSave();
        }

        public void LoadData()
        {
            mainGameData = new GameData();
            Load();
            Debug.Log("Main data coins:" + mainGameData.CoinsCount);
            Debug.Log("Main data id:" + mainGameData.HeroSpriteLibraryID);
            
            
        }

        public void ShowTestData()
        {
            CloudSaveGameUI.TestHandlerText.text += "Test Main Game Data:";
            CloudSaveGameUI.TestHandlerText.text += "Coins: " + TestMainGameData.CoinsCount;
            CloudSaveGameUI.TestHandlerText.text += "ID: " + TestMainGameData.HeroSpriteLibraryID;
        }
    }
}