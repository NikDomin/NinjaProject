using System;
using Assets.Scripts.Input;
using DataPersistence;
using DataPersistence.Data;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class LevelSelector : MonoBehaviour, IDataPersistence
    {
        [SerializeField] private int level;
        [SerializeField] private bool testIsLevelPassed;
        
        private void Start()
        {
            NewInputManager.PlayerInput.SwitchCurrentActionMap("UI");
            
            GetComponentInChildren<TextMeshProUGUI>().text = level.ToString();
        }

        private void OnValidate()
        {
            GetComponentInChildren<TextMeshProUGUI>().text = level.ToString();
        }

        public void LoadScene()
        {
            //SceneManager.LoadScene("Level 1");
            SceneManager.LoadScene("Level " + level.ToString());
        }

        public void LoadData(GameData data)
        {
            data.LevelPassed.TryGetValue(level, out testIsLevelPassed);
            
        }

        public void SaveData(GameData data)
        {
            
        }
    }
}