using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utils;

namespace UI
{
    public class CompleteLevel : Panel
    {
        [SerializeField] private Button homeButton;
        [SerializeField] private Button nextLevelButton;
        

        private void OnEnable()
        {
            TimeManager.Instance.PauseGame();
            
            homeButton.onClick.AddListener(ToMainMenu);
            nextLevelButton.onClick.AddListener(ToNextLevel);
        }
        private void OnDisable()
        {
            TimeManager.Instance.UnpauseGame();
            
            homeButton.onClick.RemoveListener(ToMainMenu);
            nextLevelButton.onClick.RemoveListener(ToNextLevel);

        }

        public override void EnablePanel()
        {
            base.EnablePanel();
        }

        public override void DisablePanel()
        {
            base.DisablePanel();
        }

        #region ButtonsListeners

        private void ToMainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }

        private void ToNextLevel()
        {
            SceneManager.LoadScene("Level " + (GameUtils.SceneNumber(SceneManager.GetActiveScene())+1));
        }

        #endregion
    }
}