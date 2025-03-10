using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DataPersistence.Data;
using Level;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.U2D.Animation;
using Utils;

namespace DataPersistence
{
    public class DataPersistenceManager : MonoBehaviour
    {
        public event Action OnLoadEndSuccefully;

        #region SerializeField

        [Header("Debugging")] 
        [SerializeField] private bool disableDataPersistence;
        
        [Header("File Storage Config")] 
        [SerializeField] private string fileName;

        [SerializeField] private bool useEncryption;

        [Header("Default values")]
        [SerializeField] private SpriteLibraryAsset defaultHeroSpriteLibraryAsset;

        [SerializeField]private LoadScreen loadScreen;

        #endregion

        #region PublicFields

        public SpriteLibraryAsset DefaultHeroSpriteLibraryAsset
        {
            get { return defaultHeroSpriteLibraryAsset; }
            private set { defaultHeroSpriteLibraryAsset = value; }
        }
        public FileDataHandler dataHandler { get; private set; }
        // public CloudDataHandler CloudDataHandler { get; private set; }
        
        public GameData gameData;
        // public GameData GameDataToLoad;
        public bool IsSaved;
        public static DataPersistenceManager instance { get; private set; }

        #endregion
        
        #region PrivateFields

        private List<IDataPersistence> dataPersistenceObjects;
        private CloudDataHandler cloudDataHandler;

        #endregion
        
        #region Mono

        private void Awake()
        {
            
            if (instance != null)
            {
                Debug.LogError("Found more than one Data Persistence Manager in the scene. Destroying the newest one.");
                Destroy(gameObject);
                
                return;
            }
            if(disableDataPersistence)
                Debug.LogWarning("Data Persistence is currently disabled");
            
            instance = this;
            DontDestroyOnLoad(gameObject);
            
            
            dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, useEncryption);
            cloudDataHandler = new CloudDataHandler();
            // CloudDataHandler = new CloudDataHandler(DataSource.ReadNetworkOnly,
            //     ConflictResolutionStrategy.UseMostRecentlySaved, "saveData");
            loadScreen = FindObjectOfType<LoadScreen>();
        }
        
        private void OnEnable()
        {
            loadScreen.ShowLoadScreen();
            SceneManager.sceneLoaded += OnSceneLoaded;
            cloudDataHandler.OnSaveCallback += CloudSaveCallback;
            
        }
        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            if(cloudDataHandler != null) 
                cloudDataHandler.OnSaveCallback -= CloudSaveCallback;
        }
        
        // void OnApplicationPause(bool pauseStatus)
        // {
        //     if (pauseStatus)
        //     {
        //         var levelPlayAds = FindObjectOfType<LevelPlayAds>();
        //         if (levelPlayAds == null)
        //             return;
        //         if (levelPlayAds.isAdsPlaying)
        //             return;
        //         //Save
        //         StartCoroutine(SaveWhenFocusLost());
        //     }
        // }
        
        // private void OnApplicationQuit()
        // {
        //     SaveGame();
        // }        

        #endregion
        
        #region LoadGame

        public void LoadGame()
        {
            if(TimeManager.Instance != null) 
                TimeManager.Instance.PauseGame();
            
            Debug.Log("Load Game Start");
            if (disableDataPersistence)
            {
                Debug.LogWarning("Data Persistence has disable flag");
                loadScreen.HideLoadScreen();
                if(TimeManager.Instance != null) 
                    TimeManager.Instance.UnpauseGame();
                return;
                
            }

            if (Social.localUser.authenticated)
            {
                Debug.Log("Local User authenticated, try load from cloud");
                //Try get data from cloud
                // CloudSaveGameUI.Instance.LogText.text += "CloudLoadGame from manager";
                cloudDataHandler.LoadData();
                /////
                // gameData = GameDataToLoad;
                // CloudSaveGameUI.Instance.LogText.text += "After load game data:";
                // CloudSaveGameUI.Instance.LogText.text += "coins count: " + gameData.CoinsCount;
                

            }
            else
            {
                //Load data from a file
                // CloudSaveGameUI.Instance.LogText.text += "Load from jsonfile";

                Debug.Log("User is not authenticated, try load from disk");
                gameData = dataHandler.Load();
                if(gameData == null)
                    NewGame();
                LoadToObjects(gameData);
            }
            

            
        }
        public void LoadToObjects(GameData gameData)
        {
            Debug.Log("Load to objects");
            // CloudSaveGameUI.Instance.LogText.text += " Load To Objects";
            if (gameData == null)
            {
                Debug.LogWarning("No data was found. Init data to defaults.");
                gameData = new GameData();
            }
            foreach (var item in dataPersistenceObjects)
            {
                item.LoadData(gameData);
            }
            Debug.Log("HideLoadScreen");
            loadScreen.HideLoadScreen();
            Debug.Log("OnLoadEnd");
            OnLoadEndSuccefully?.Invoke();
           
            //if on choose level scene
            var ChooseLevel = FindObjectOfType<ChooseLevel>();
            if (ChooseLevel != null)
            {
                ChooseLevel.FindLevelNeedToPass();
            }
            if(TimeManager.Instance != null) 
                TimeManager.Instance.UnpauseGame();
        }

        #endregion

        #region SaveGame

        public void SaveGame()
        {
            if (disableDataPersistence)
                return;
            loadScreen.ShowLoadScreen();
            
            // if we don't have any AnimationData to save, log a warning here
            if (this.gameData == null)
            {
                Debug.LogWarning("No AnimationData was found. A New Game needs to be started before AnimationData can be saved.");
                gameData = new GameData();
                loadScreen.HideLoadScreen();
            }
            
            foreach (var item in dataPersistenceObjects)
            {
                item.SaveData(gameData);
            }

            
            
            if (Social.localUser.authenticated)
            {
                // CloudSaveGameUI.Instance.LogText.text += "Cloud save from manager";
                cloudDataHandler.Save(gameData);
                // IsSaved = true;
            }

            if (!Social.localUser.authenticated)
            {
                // CloudSaveGameUI.Instance.LogText.text += "save to drive";
                dataHandler.Save(gameData);
                IsSaved = true;
                // loadScreen.HideLoadScreen();
            }
        }
        private void CloudSaveCallback(bool isSavedToCloud)
        {
            // CloudSaveGameUI.Instance.LogText.text += "Cloud Save Callback with: " + isSavedToCloud;
            IsSaved = isSavedToCloud;
            //if saved to cloud failed
            if (!IsSaved)
            {
                //save to disk 
                // CloudSaveGameUI.Instance.LogText.text += "save to drive";
                dataHandler.Save(gameData);
                IsSaved = true;
            }
            // loadScreen.HideLoadScreen();
        }

        private IEnumerator SaveWhenFocusLost()
        {
            SaveGame();
            yield return new WaitUntil(() => IsSaved);
            IsSaved = false;
            loadScreen.HideLoadScreen();
        }

        #endregion

        #region PrivateMethods

        private List<IDataPersistence> FindAllDataPersistenceObjects()
        {
            //Find all the scripts that implement the AnimationData saving interface
            // FindObjectsofType takes in an optional boolean to include inactive gameobjects
            IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>(true)
                .OfType<IDataPersistence>();

            return new List<IDataPersistence>(dataPersistenceObjects);
        }
        
        private void NewGame()
        {
            gameData = new GameData();
        }

        
        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            
            loadScreen = FindObjectOfType<LoadScreen>();
            Debug.Log("OnSceneLoaded");
            // CloudSaveGameUI.Instance.LogText.text += "On Scene Loaded ";
            dataPersistenceObjects = FindAllDataPersistenceObjects();
            
            // if on main menu and not authenticated - return
            var sceneName = SceneManager.GetActiveScene().name;
            if(sceneName == "MainMenu" && !Social.localUser.authenticated)
                return;
            
            LoadGame();
        }

        #endregion

        #if UNITY_EDITOR
        public void CreateFileDataHandler()
        {
            dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, useEncryption);
        }
        #endif
    }
}