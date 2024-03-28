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

        [Header("For editor only")] 
        [SerializeField]
        private bool isHasTextInChildren = true;
        
        private void Start()
        {
            NewInputManager.PlayerInput.SwitchCurrentActionMap("UI");
            
            if(isHasTextInChildren)
                GetComponentInChildren<TextMeshProUGUI>().text = levelName;
        }

        private void OnValidate()
        {
            if(isHasTextInChildren)
                GetComponentInChildren<TextMeshProUGUI>().text = levelName;
        }

        public void LoadLevel()
        {
            //Save Game
            DataPersistenceManager.instance.SaveGame();
            SceneManager.LoadScene("Level " + levelName);
        }

        public void LoadScene()
        {
            //Save Game
            DataPersistenceManager.instance.SaveGame();
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