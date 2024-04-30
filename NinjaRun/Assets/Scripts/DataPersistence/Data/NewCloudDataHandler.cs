using System;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
using Services;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = System.Random;

namespace DataPersistence.Data
{
    public class NewCloudDataHandler : MonoBehaviour
    {
        // private bool isSaving;
        //
        // private GameData gameDataToLoad;
        // private GameData gameDataToSave;
        //
        // private GameData mainGameData;
        //
        // //
        // public CloudSaveGameUI CloudSaveGameUI;
        // public TestHandler TestHandler;
        //
        // public static NewCloudDataHandler Instance;
        // // private GameData gameDataToLoad;
        // // private GameData gameDataToSave;
        //
        // // private void Awake()
        // // {
        // //     gameDataToLoad = new GameData();
        // // }
        // private void Awake()
        // {
        //     if (Instance != null)
        //     {
        //         Debug.LogError("Found more than one new cloud data handler in the scene. Destroying the newest one.");
        //         Destroy(gameObject);
        //         return;
        //     }
        //     
        //     Instance = this;
        //    
        //     DontDestroyOnLoad(gameObject);
        //    
        // }
        // private void OnEnable()
        // {
        //     SceneManager.sceneLoaded += OnSceneLoaded;
        //     
        // }
        //
        // private void OnDisable()
        // {
        //     SceneManager.sceneLoaded -= OnSceneLoaded;
        //     
        // }
        //
        // private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        // {
        //     // mainGameData = new GameData();
        //     // CloudSaveGameUI = UI.CloudSaveGameUI.Instance;
        //     // Load();
        //     // // mainGameData = Load();
        //     // CloudSaveGameUI.OutputText.text = "";
        //     // CloudSaveGameUI.OutputText.text += "Hero sprite id: " + mainGameData.HeroSpriteLibraryID;
        //     // CloudSaveGameUI.OutputText.text += "Coin count: " + mainGameData.CoinsCount;
        //     // CloudSaveGameUI.OutputText.text += "level need to pass: " + mainGameData.levelNeedToPass;
        // }
        //
        // public void OpenSave(bool saving)
        // {
        //     CloudSaveGameUI.LogText.text += "";
        //     CloudSaveGameUI.LogText.text += "Open save clicked";
        //     if (Social.localUser.authenticated)
        //     {
        //         CloudSaveGameUI.LogText.text += "User is authenticated";
        //         isSaving = saving;
        //         ((PlayGamesPlatform)Social.Active).SavedGame.OpenWithAutomaticConflictResolution("MyFileName", DataSource.ReadCacheOrNetwork, ConflictResolutionStrategy.UseLongestPlaytime, OnSavedGameOpened);
        //     }
        // }
        //
        // public void OnSavedGameOpened(SavedGameRequestStatus status, ISavedGameMetadata meta) {
        //     if (status == SavedGameRequestStatus.Success) {
        //         if (isSaving)
        //         {
        //             CloudSaveGameUI.LogText.text += "Status successful, attempting to save...";
        //             //convert to byte array
        //             byte[] myData = System.Text.ASCIIEncoding.ASCII.GetBytes(GetSaveString());
        //
        //             //metadata
        //             SavedGameMetadataUpdate updateForMetadata = new SavedGameMetadataUpdate.Builder()
        //                 .WithUpdatedDescription("Appdate game data at: " + DateTime.Now.ToString()).Build();
        //   
        //             //saving
        //             ((PlayGamesPlatform)Social.Active).SavedGame.CommitUpdate(meta, updateForMetadata, myData, SaveCallback);
        //         }
        //         else
        //         {
        //             CloudSaveGameUI.LogText.text += "Status successful, attempting to load...";
        //             // //loading
        //             ((PlayGamesPlatform)Social.Active).SavedGame.ReadBinaryData(meta, LoadGameCallback);
        //         }
        //     } else {
        //         Debug.LogError("Error when try Open SavedGame");
        //     }
        // }
        //
        // private void LoadGameCallback(SavedGameRequestStatus status, byte[] bytes)
        // {
        //     if (status == SavedGameRequestStatus.Success)
        //     {
        //         CloudSaveGameUI.LogText.text += "load successful, attempting to read data...";
        //         string loadedData = System.Text.ASCIIEncoding.ASCII.GetString(bytes);
        //         CloudSaveGameUI.JsonText.text += "String from bytes: " + loadedData;
        //         //Name|age
        //         LoadSaveString(loadedData);
        //     }
        // }
        //
        // private void LoadSaveString(string loadedData)
        // {
        //     gameDataToLoad = JsonUtility.FromJson<GameData>(loadedData);
        //     TestHandler.TestMainGameData = gameDataToLoad;
        //     mainGameData = gameDataToLoad;
        //     CloudSaveGameUI.JsonText.text += "";
        //     CloudSaveGameUI.JsonText.text += "Data From Json: ";
        //     CloudSaveGameUI.JsonText.text += "Coins From Json: " + mainGameData.CoinsCount.ToString();
        // }
        //
        // private string GetSaveString()
        // {
        //     return JsonUtility.ToJson(gameDataToSave,true);
        // }
        //
        // private void SaveCallback(SavedGameRequestStatus status, ISavedGameMetadata metadata)
        // {
        //     if (status == SavedGameRequestStatus.Success)
        //     {
        //         CloudSaveGameUI.LogText.text += "Successfully save to the cloud";
        //     }
        //     else
        //     {
        //         CloudSaveGameUI.LogText.text += "Fail save to the cloud";
        //     }
        // }
        //
        // public void Load()
        // {
        //     gameDataToLoad = null;
        //     OpenSave(false);
        //     ///////////////////////
        //     CloudSaveGameUI.JsonText.text += "Open Save End";
        //     CloudSaveGameUI.JsonText.text += " ";
        //     CloudSaveGameUI.JsonText.text += "Data From LoadData: ";
        //     CloudSaveGameUI.JsonText.text += "Coins From LoadData: " + gameDataToLoad.CoinsCount.ToString();
        //     // return gameDataToLoad;
        // }
        //
        // public void Save()
        // {
        //     gameDataToSave = new GameData(); 
        //     CloudSaveGameUI.LogText.text += "Save game clicked";
        //     Random random = new Random();
        //     gameDataToSave.CoinsCount = random.Next(100, 200);
        //     CloudSaveGameUI.LogText.text += "New random coins count: " + gameDataToSave.CoinsCount.ToString();
        //     gameDataToSave.HeroSpriteLibraryID = random.Next(0, 15);
        //     CloudSaveGameUI.LogText.text += "New random sprite id: " + gameDataToSave.HeroSpriteLibraryID.ToString();
        //
        //     OpenSave(true);
        // }
        //
        // public void LoadData()
        // {
        //     mainGameData = new GameData();
        //     CloudSaveGameUI.LogText.text += "Load game clicked";
        //     Load();
        //     ///////////////
        //     CloudSaveGameUI.OutputText.text += "";
        //     CloudSaveGameUI.OutputText.text += "Hero sprite id: " + mainGameData.HeroSpriteLibraryID;
        //     CloudSaveGameUI.OutputText.text += "Coin count: " + mainGameData.CoinsCount;
        //     CloudSaveGameUI.OutputText.text += "level need to pass: " + mainGameData.levelNeedToPass;
        // }
    }
}