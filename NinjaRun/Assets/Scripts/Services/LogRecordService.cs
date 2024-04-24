using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Services
{
    public class LogRecordService : MonoBehaviour //WARNING: Do not Debug.Log() inside this class, it might create an endless loop.
    {
        public static LogRecordService Instance;
        public bool enableSave = true;
        private DataSaver dataSaver;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            
            dataSaver = new DataSaver(Application.persistentDataPath, "Logs.json");
        }

        [Serializable]
        public struct Logs
        {
            public string condition;
            public string stackTrace;
            public LogType type;

            public string dateTime;

            public Logs(string condition, string stackTrace, LogType type, string dateTime)
            {
                this.condition = condition;
                this.stackTrace = stackTrace;
                this.type = type;
                this.dateTime = dateTime;
            }
        }
        
        [Serializable]
        public class LogInfo
        {
            public List<Logs> logInfoList = new List<Logs>();
        }

        LogInfo logs = new LogInfo();
        
        void OnEnable()
        {

            //Subscribe to Log Event
            Application.logMessageReceived += LogCallback;
        }
        void OnDisable()
        {
            //Un-Subscribe from Log Event
            Application.logMessageReceived -= LogCallback;
        }
        
        //Called when there is an exception
        void LogCallback(string condition, string stackTrace, LogType type)
        {
            //Create new Log
            Logs logInfo = new Logs(condition, stackTrace, type, DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz"));

            //Add it to the List
            logs.logInfoList.Add(logInfo);
        }
        
        
        //Save log  when focous is lost
        void OnApplicationFocus(bool hasFocus)
        {
            if (!hasFocus)
            {
                //Save
                if (enableSave)
                    dataSaver.Save(logs);
            }
        }
        
        //Save log on exit
        void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus)
            {
                //Save
                if (enableSave)
                    dataSaver.Save(logs);
            }
        }
        
    }

    public class DataSaver
    {
        private string dataDirPath = "";
        private string dataFileName = "";

        public DataSaver(string dataDirPath, string dataFileName)
        {
            this.dataDirPath = dataDirPath;
            this.dataFileName = dataFileName;
        }
        
        public void Save(LogRecordService.LogInfo logInfo)
        {
            string fullPath = Path.Combine(dataDirPath, dataFileName);
            try
            {   
                //Create directory if it dosnt already exist
                Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
                // serialize the C# game AnimationData object into Json
                string dataToStore = JsonUtility.ToJson(logInfo, true);
                
                // write the serialized AnimationData to the file
                using (FileStream stream = new FileStream(fullPath, FileMode.Create))
                {
                    using (StreamWriter writer = new StreamWriter(stream))
                    {
                        writer.Write(dataToStore);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogError("Error occured when trying to save data to file: " + fullPath + "\n" + e);
            }
        }
    }

    
  
}