using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
using Services;
using UnityEngine;

namespace DataPersistence.Data
{
    public class CloudDataHandler
    {
        private DataSource dataSource;
        private ConflictResolutionStrategy conflicts;
        private string saveName = "";
        private BinaryFormatter binaryFormatter;

        private PlayerProfile playerProfile;

        private GameData rawData;

        private GameData dataToLoad;
        // private BackgroundController backgroundController;

        public CloudDataHandler(DataSource dataSource, ConflictResolutionStrategy conflicts, string saveName)
        {
            this.dataSource = dataSource;
            this.conflicts = conflicts;
            this.saveName = saveName;

            binaryFormatter = new BinaryFormatter();
        }

        public GameData Load()
        {
            OpenCloudSave(OnLoadResponse);
            return dataToLoad;
        }

        private void OnLoadResponse(SavedGameRequestStatus status, ISavedGameMetadata metadata)
        {
            if (status == SavedGameRequestStatus.Success)
            {
                PlayGamesPlatform.Instance.SavedGame.ReadBinaryData(metadata, LoadCallback);
            }
            else
            {
                //return to use local data
                dataToLoad = null;
                return;
            }
        }

        private void LoadCallback(SavedGameRequestStatus status, byte[] bytes)
        {
            if (status == SavedGameRequestStatus.Success)
            {
                ApplyCloudData(DeserializeSaveData(bytes), bytes.Length > 0);
            }
            else
            {
                dataToLoad = null;
                //use local data
            }
        }

        #region Save

        public void Save(GameData data)
        {
            rawData = data;
            OpenCloudSave(OnSaveResponse);
            rawData = null;
        }

        private void OnSaveResponse(SavedGameRequestStatus status, ISavedGameMetadata metadata)
        {
            if (status == SavedGameRequestStatus.Success)
            {
                if (rawData == null)
                {
                    Debug.LogWarning("Raw Data is null");
                    return;
                }

                var data = SerializeSaveData(rawData);
                if (data == null)
                {
                    Debug.LogWarning("SerializeSaveData is null");
                    return;
                }

                var update = new SavedGameMetadataUpdate.Builder().Build(); 
                //Save Data
                PlayGamesPlatform.Instance.SavedGame.CommitUpdate(metadata,update,data, SaveCallback);
                
            }
            else
            {
                Debug.LogError("OnSaveResponse error");
            }
        }

        private void SaveCallback(SavedGameRequestStatus status, ISavedGameMetadata metadata)
        {
            if(status != SavedGameRequestStatus.Success)
                Debug.LogWarning("Data is not saved because of some error");
                
        }

        #endregion

        private void OpenCloudSave(Action<SavedGameRequestStatus, ISavedGameMetadata> callbadck)
        {
            ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;

            if (!Social.localUser.authenticated || string.IsNullOrEmpty(saveName))
                Debug.LogError("OpenCloudSave Error");

            savedGameClient.OpenWithAutomaticConflictResolution(saveName, dataSource, conflicts, callbadck);

        }


        private byte[] SerializeSaveData(GameData gameData)
        {
            string json = JsonUtility.ToJson(gameData);
            try
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    binaryFormatter.Serialize(memoryStream, json);
                    return memoryStream.GetBuffer();
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                return null;
            }
        }

        private GameData DeserializeSaveData(byte[] bytes)
        {
            if (bytes == null || bytes.Length == 0)
                return null;
            try
            {
                using (MemoryStream memoryStream = new MemoryStream(bytes))
                {
                    string json = (string)binaryFormatter.Deserialize(memoryStream);
                    return JsonUtility.FromJson<GameData>(json);
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                return null;
            }
        }

        public void ApplyCloudData(GameData gameData, bool dataExists)
        {
            if (!dataExists)
            {
                dataToLoad = null;
                return;//use local data
            }

            dataToLoad = gameData;
            //load data
        }
    }
}