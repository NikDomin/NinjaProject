using System;
using Assets.Scripts.Input;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class LevelSelector : MonoBehaviour
    {
        [SerializeField] private int level;
        
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
    }
}