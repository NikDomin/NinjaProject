using DataPersistence;
using DataPersistence.Data;
using Input;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace UI
{
    public class LevelSelector : MonoBehaviour, IDataPersistence
    {
        [FormerlySerializedAs("level")] [SerializeField] private string levelName;
        [SerializeField] private bool testIsLevelPassed;
        
        private void Start()
        {
            NewInputManager.PlayerInput.SwitchCurrentActionMap("UI");

            GetComponentInChildren<TextMeshProUGUI>().text = levelName;
        }

        private void OnValidate()
        {
            GetComponentInChildren<TextMeshProUGUI>().text = levelName;
        }

        public void LoadLevel()
        {
            //SceneManager.LoadScene("Level 1");
            SceneManager.LoadScene("Level " + levelName);
        }

        public void LoadScene()
        {
            SceneManager.LoadScene(levelName);
        }

        public void LoadData(GameData data)
        {
            data.LevelPassed.TryGetValue(levelName, out testIsLevelPassed);
            
        }

        public void SaveData(GameData data)
        {
            
        }
    }
}