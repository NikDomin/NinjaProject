using DataPersistence;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class GameOverPanel : Panel
    {
        [SerializeField] private Button homeButton;
        [SerializeField] private Button restartButton;
        
        public override void EnablePanel()
        {
            base.EnablePanel();

            homeButton.onClick.AddListener(ToMainMenu);
            restartButton.onClick.AddListener(ResetLevel);
        }

        public override void DisablePanel()
        {
            base.DisablePanel();
            
            homeButton.onClick.RemoveListener(ToMainMenu);
            restartButton.onClick.RemoveListener(ResetLevel);
        }


        #region ButtonsListeners

        private void ResetLevel()
        {
            DisablePanel();
            DataPersistenceManager.instance.SaveGame();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void ToMainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }

        #endregion
    }
}