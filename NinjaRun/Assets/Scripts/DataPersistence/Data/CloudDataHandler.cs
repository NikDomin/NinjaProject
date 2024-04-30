using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
using Level;
using Services;
using UI;
using UnityEngine;

namespace DataPersistence.Data
{
    public class CloudDataHandler
    {

        public event Action<bool> OnSaveCallback;
        private bool isSaving;
        private GameData gameDataToSave;
        private GameData gameDataToLoad;
        private GameData mainGameData;

        public CloudDataHandler()
        {
           
        }
        
        public void OpenSave(bool saving)
        {
            CloudSaveGameUI.Instance.LogText.text += "";
            CloudSaveGameUI.Instance.LogText.text += "Open save clicked";
            if (Social.localUser.authenticated)
            {
                CloudSaveGameUI.Instance.LogText.text += "User is authenticated";
                isSaving = saving;
                ((PlayGamesPlatform)Social.Active).SavedGame.OpenWithAutomaticConflictResolution("MyFileName", DataSource.ReadCacheOrNetwork, ConflictResolutionStrategy.UseLongestPlaytime, OnSavedGameOpened);
            }
        }
        private void OnSavedGameOpened(SavedGameRequestStatus status, ISavedGameMetadata meta) {
            if (status == SavedGameRequestStatus.Success) {
                if (isSaving)
                {
                    CloudSaveGameUI.Instance.LogText.text += "Status successful, attempting to save...";
                    //convert to byte array
                    byte[] myData = System.Text.ASCIIEncoding.ASCII.GetBytes(GetSaveString());

                    //metadata
                    SavedGameMetadataUpdate updateForMetadata = new SavedGameMetadataUpdate.Builder()
                        .WithUpdatedDescription("Appdate game data at: " + DateTime.Now.ToString()).Build();
          
                    //saving
                    ((PlayGamesPlatform)Social.Active).SavedGame.CommitUpdate(meta, updateForMetadata, myData, SaveCallback);
                }
                else
                {
                    CloudSaveGameUI.Instance.LogText.text += "Status successful, attempting to load...";
                    // //loading
                    ((PlayGamesPlatform)Social.Active).SavedGame.ReadBinaryData(meta, LoadGameCallback);
                }
            } else {
                Debug.LogError("Error when try Open SavedGame");
            }
        }
        private void LoadGameCallback(SavedGameRequestStatus status, byte[] bytes)
        {
            if (status == SavedGameRequestStatus.Success)
            {
                CloudSaveGameUI.Instance.LogText.text += "load successful, attempting to read data...";
                string loadedData = System.Text.ASCIIEncoding.ASCII.GetString(bytes);
                CloudSaveGameUI.Instance.JsonText.text += "String from bytes: " + loadedData;
                //Name|age
                LoadSaveString(loadedData);
            }
        }
        private void LoadSaveString(string loadedData)
        {
            gameDataToLoad = JsonUtility.FromJson<GameData>(loadedData);
            // DataPersistenceManager.instance.GameDataToLoad = gameDataToLoad;
            DataPersistenceManager.instance.gameData = gameDataToLoad;
            DataPersistenceManager.instance.LoadToObjects(gameDataToLoad);
            
            TestHandler.Instance.TestMainGameData = gameDataToLoad;
            mainGameData = gameDataToLoad;
            CloudSaveGameUI.Instance.JsonText.text += "";
            CloudSaveGameUI.Instance.JsonText.text += "Data From Json: ";
            CloudSaveGameUI.Instance.JsonText.text += "Coins From Json: " + mainGameData.CoinsCount.ToString();
        }

        private string GetSaveString()
        {
            return JsonUtility.ToJson(gameDataToSave,true);
        }
        
        private void SaveCallback(SavedGameRequestStatus status, ISavedGameMetadata metadata)
        {
            bool isSave;
            if (status == SavedGameRequestStatus.Success)
            {
                // DataPersistenceManager.instance.IsSaved = true;
                isSave = true;
                CloudSaveGameUI.Instance.LogText.text += "Successfully save to the cloud";
                
            }
            else
            {
                isSave = false;
                CloudSaveGameUI.Instance.LogText.text += "Fail save to the cloud";
            }
            OnSaveCallback?.Invoke(isSave);
        }
        private void Load()
        {
            gameDataToLoad = null;
            OpenSave(false);
            ///////////////////////
            CloudSaveGameUI.Instance.JsonText.text += "Open Save End";
            CloudSaveGameUI.Instance.JsonText.text += " ";
            CloudSaveGameUI.Instance.JsonText.text += "Data From LoadData: ";
            CloudSaveGameUI.Instance.JsonText.text += "Coins From LoadData: " + gameDataToLoad.CoinsCount.ToString();
            // return gameDataToLoad;
        }

        public void Save(GameData gameData)
        {
            gameDataToSave = gameData; 
            CloudSaveGameUI.Instance.LogText.text += "Save game clicked";
            OpenSave(true);
        }

        public void LoadData()
        {
            mainGameData = new GameData();
            CloudSaveGameUI.Instance.LogText.text += "Load game clicked";
            Load();
            ///////////////
            CloudSaveGameUI.Instance.OutputText.text += "";
            CloudSaveGameUI.Instance.OutputText.text += "Hero sprite id: " + mainGameData.HeroSpriteLibraryID;
            CloudSaveGameUI.Instance.OutputText.text += "Coin count: " + mainGameData.CoinsCount;
            CloudSaveGameUI.Instance.OutputText.text += "level need to pass: " + mainGameData.levelNeedToPass;
        }
    }
}