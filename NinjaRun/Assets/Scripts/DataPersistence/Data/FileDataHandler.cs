using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace DataPersistence.Data
{
    public class FileDataHandler 
    {
        private string dataDirPath = "";
        private string dataFileName = "";
        private bool useEncryption = false;
        private readonly string encryptionCodeWord = "word";
        private readonly string backupExtension = ".bak";

        public FileDataHandler(string dataDirPath, string dataFileName)
        {
            this.dataDirPath = dataDirPath;
            this.dataFileName = dataFileName;
          
        }

        public GameData Load()
        {
            string fullPath = Path.Combine(dataDirPath, dataFileName);

            GameData loadedData = null;
            if (File.Exists(fullPath))
            {
                try
                {
                    // load the serialized AnimationData from the file
                    string dataToLoad = "";
                    using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            dataToLoad = reader.ReadToEnd();
                        }
                    }

                    // deserialize the AnimationData from Json back into the C# object
                    loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
                }
                catch (Exception e)
                {
                    Debug.LogError("Error occured when trying to save data to file: " + fullPath + "\n" + e);
                    throw;
                }
            }

            return loadedData;
        }

        public void Save(GameData data)
        {
            string fullPath = Path.Combine(dataDirPath, dataFileName);
            try
            {   
                //Create directory if it dosnt already exist
                Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
                // serialize the C# game AnimationData object into Json
                string dataToStore = JsonUtility.ToJson(data, true);
                
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