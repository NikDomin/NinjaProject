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
        [SerializeField] private int LevelNumber;
        
        
        private void Awake()
        {
            var sceneName = SceneManager.GetActiveScene().name;
            string[] parts = sceneName.Split(' ');
            string digitString = parts[parts.Length - 1];
            LevelNumber = int.Parse(digitString);
        }

        public void SetLevelPassed(bool isPassed)
        {
            isLevelPassed = isPassed;
        }

        public void LoadData(GameData data)
        {
            data.LevelPassed.TryGetValue(LevelNumber, out isLevelPassed);
            if (isLevelPassed)
            {
                Debug.Log("LEVEL HAS ALREADY BEEN PASSED");
            }
        }

        public void SaveData(GameData data)
        {
            if (data.LevelPassed.ContainsKey(LevelNumber))
            {
                data.LevelPassed.Remove(LevelNumber);
            }
            data.LevelPassed.Add(LevelNumber, isLevelPassed);
        }
    }
}