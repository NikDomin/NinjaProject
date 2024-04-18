using System;
using DataPersistence;
using DataPersistence.Data;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace Level
{
    public class LevelPassedComponent : MonoBehaviour, IDataPersistence
    {
        [SerializeField] private bool isLevelPassed;
        [FormerlySerializedAs("LevelNumber")] [SerializeField] private string LevelName;
        
        
        private void Awake()
        {
            var sceneName = SceneManager.GetActiveScene().name;
            string[] parts = sceneName.Split(' ');
            string digitString = parts[parts.Length - 1];
            LevelName = digitString;
        }

        public void SetLevelPassed(bool isPassed)
        {
            isLevelPassed = isPassed;
        }

        public void LoadData(GameData data)
        {
            data.LevelPassed.TryGetValue(LevelName, out isLevelPassed);
            if (isLevelPassed)
            {
                Debug.Log("LEVEL HAS ALREADY BEEN PASSED");
            }
        }

        public void SaveData(GameData data)
        {
            if (data.LevelPassed.ContainsKey(LevelName))
            {
                data.LevelPassed.Remove(LevelName);
            }
            data.LevelPassed.Add(LevelName, isLevelPassed);

            if (isLevelPassed && LevelName == data.levelNeedToPass)
            {
                int levelPassed = Convert.ToInt32(LevelName);
                levelPassed++;
                data.levelNeedToPass = levelPassed.ToString();
            }
        }
    }
}