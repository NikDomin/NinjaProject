using System;
using System.Collections.Generic;
using System.Linq;
using DataPersistence.Data;
using UnityEngine;

namespace DataPersistence
{
    public class DataPersistenceManager : MonoBehaviour
    {
        [Header("File Storage Config")] 
        [SerializeField] private string fileName;

        [SerializeField] private bool useEncryption;
        
        private GameData gameData;
        private FileDataHandler dataHandler;
        private List<IDataPersistence> dataPersistenceObjects;
        
        
        public static DataPersistenceManager instance { get; private set; }

        
        
        #region Mono

        private void Start()
        {
            
            dataPersistenceObjects = FindAllDataPersistenceObjects();
            LoadGame();
        }

        private void OnApplicationQuit()
        {
            SaveGame();
        }        
        
        private void Awake()
        {
            dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, useEncryption);
            
            if (instance != null)
            {
                Debug.LogError("Found more than one Data Persistence Manager in the scene. Destroying the newest one.");
                Destroy(this.gameObject);
                return;
            }
            instance = this;
            // DontDestroyOnLoad(this.gameObject);
            //
            // if (disableDataPersistence)
            // {
            //     Debug.LogWarning("Data persistence is currently disabled!");
            // }
            // this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, useEncryption);//Application.persistentDataPath standard Unity AnimationData storage directory
            //
            // InitializeSelectedProfileID();
            
        }

        #endregion
        
        

        public void NewGame()
        {
            gameData = new GameData();
        }

        public void LoadGame()
        {
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