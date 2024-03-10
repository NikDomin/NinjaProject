using System;
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

        public FileDataHandler(string dataDirPath, string dataFileName, bool useEncryption)
        {
            this.dataDirPath = dataDirPath;
            this.dataFileName = dataFileName;
            this.useEncryption = useEncryption;

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

                    if (useEncryption)
                        dataToLoad = EncryptDecrypt(dataToLoad);
                    
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

                if (useEncryption)
                    dataToStore = EncryptDecrypt(dataToStore);
                
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

        public void DeleteSaveFile()
        {
            string fullPath = Path.Combine(dataDirPath, dataFileName);
            Debug.LogWarning("Delete Save File. Path: " + fullPath);
            File.Delete(fullPath);
        }

    
        
        private string EncryptDecrypt(string data)
        {
            string modifiedData = "";
            for (int i = 0; i < data.Length; i++)
            {
                modifiedData += (char)(data[i] ^ encryptionCodeWord[i % encryptionCodeWord.Length]);
            }
            return modifiedData;
        }
        
    }
}