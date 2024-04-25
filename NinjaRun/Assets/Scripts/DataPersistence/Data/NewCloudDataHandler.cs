using System;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
using UI;
using UnityEngine;

namespace DataPersistence.Data
{
    public class NewCloudDataHandler : MonoBehaviour
    {
        private bool isSaving;
        public CloudSaveGameUI CloudSaveGameUI;
        
        public void OpenSave(bool saving)
        {
            CloudSaveGameUI.LogText.text += "";
            CloudSaveGameUI.LogText.text += "Open save clicked";
            if (Social.localUser.authenticated)
            {
                CloudSaveGameUI.LogText.text += "User is authenticated";
                isSaving = saving;
                ((PlayGamesPlatform)Social.Active).SavedGame.OpenWithAutomaticConflictResolution("MyFileName", DataSource.ReadCacheOrNetwork, ConflictResolutionStrategy.UseLongestPlaytime, OnSavedGameOpened);
            }
        }
        
        public void OnSavedGameOpened(SavedGameRequestStatus status, ISavedGameMetadata meta) {
            if (status == SavedGameRequestStatus.Success) {
                if (isSaving)
                {
                    CloudSaveGameUI.LogText.text += "Status successful, attempting to save...";
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
                    CloudSaveGameUI.LogText.text += "Status successful, attempting to load...";
                    //loading
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
                CloudSaveGameUI.LogText.text += "load successful, attempting to read data...";
                string loadedData = System.Text.ASCIIEncoding.ASCII.GetString(bytes);
                //Name|age
                LoadSaveString(loadedData);
            }
        }

        private void LoadSaveString(string loadedData)
        {
            string[] cloudStringArray = loadedData.Split('|');
            //CloudStringArray[0] = name
            //CloudStringArray[1] = age
            CloudSaveGameUI.MyName = cloudStringArray[0];
            CloudSaveGameUI.Age = int.Parse(cloudStringArray[1]);

            CloudSaveGameUI.OutputText.text = "";
            CloudSaveGameUI.OutputText.text += "My name is:" + CloudSaveGameUI.MyName;
            CloudSaveGameUI.OutputText.text += "My age is:" + CloudSaveGameUI.Age;
        }

        private string GetSaveString()
        {
            string data = "";

            data += CloudSaveGameUI.MyName;
            data += "|";
            data += CloudSaveGameUI.Age;
            
            return data;
        }

        private void SaveCallback(SavedGameRequestStatus status, ISavedGameMetadata metadata)
        {
            if (status == SavedGameRequestStatus.Success)
            {
                CloudSaveGameUI.LogText.text += "Successfully save to the cloud";
            }
            else
            {
                CloudSaveGameUI.LogText.text += "Fail save to the cloud";
            }
        }
    }
}