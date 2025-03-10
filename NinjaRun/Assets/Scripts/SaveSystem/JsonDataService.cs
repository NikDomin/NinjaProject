using System;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace SaveSystem
{
    public class JsonDataService : IDataService
    {
        public bool SaveData<T>(string RelativePath, T Data, bool Encrypted)
        {
            string path = Application.persistentDataPath + RelativePath;
           
                try
                {
                    if (File.Exists(path))
                    {
                        Debug.Log("Data exists. Deleting old file and writing a new one!");
                        File.Delete(path);
                    }
                    else
                    {
                        Debug.Log("Writing file for the first time");   
                    }
                    using FileStream stream = File.Create(path);
                    stream.Close();
                    
                    File.WriteAllText(path, JsonConvert.SerializeObject(Data));
                    return true;
                }
                catch (Exception e)
                { 
                    Debug.LogError($"Unable to save data due to: {e.Message} {e.StackTrace}");
                    return false;
                }
           
        }

        public T LoadData<T>(string RelativePath, bool Encrypted)
        {
            throw new System.NotImplementedException();
        }
    }
}