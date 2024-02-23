using System;
using System.Collections.Generic;
using System.Linq;
using DataPersistence.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DataPersistence
{
    public class DataPersistenceManager : MonoBehaviour
    {
        [Header("Debugging")] 
        [SerializeField] private bool disableDataPersistence = false;
        
        [Header("File Storage Config")] 
        [SerializeField] private string fileName;

        [SerializeField] private bool useEncryption;
        
        private GameData gameData;
        private FileDataHandler dataHandler;
        private List<IDataPersistence> dataPersistenceObjects;
        
        
        public static DataPersistenceManager instance { get; private set; }

        
        
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
        }
        
        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            
        }
        
        private void Start()
        {
            
         
        }

        private void OnApplicationQuit()
        {
            SaveGame();
        }        

        #endregion
        
        

        private void NewGame()
        {
            gameData = new GameData();
        }

        
        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            Debug.Log("OnSceneLoaded");
            dataPersistenceObjects = FindAllDataPersistenceObjects();
            LoadGame();
        }
        
        private void LoadGame()
        {
            if (disableDataPersistence)
                return;
            
            //Load data from a file
            gameData = dataHandler.Load();
            
            if (this.gameData == null)
            {
                Debug.LogWarning("No data was found. Init data to defaults.");
                NewGame();
            }

            foreach (var item in dataPersistenceObjects)
            {
                item.LoadData(gameData);
            }

        }

        public void SaveGame()
        {
            if (disableDataPersistence)
                return;
            
            // if we don't have any AnimationData to save, log a warning here
            if (this.gameData == null)
            {
                Debug.LogWarning("No AnimationData was found. A New Game needs to be started before AnimationData can be saved.");
                return;
            }
            
            foreach (var item in dataPersistenceObjects)
            {
                item.SaveData(gameData);
            }
            

            dataHandler.Save(gameData);
        }

        private List<IDataPersistence> FindAllDataPersistenceObjects()
        {
            //Find all the scripts that implement the AnimationData saving interface
            // FindObjectsofType takes in an optional boolean to include inactive gameobjects
            IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>(true)
                .OfType<IDataPersistence>();

            return new List<IDataPersistence>(dataPersistenceObjects);
        }
       
    }
}