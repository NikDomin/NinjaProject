using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Utils;

namespace UI
{
    public class PausePanel : Panel
    {
        [SerializeField] private Panel settingsPanel;
        [Header("Buttons")]
        [SerializeField] private Button continueButton;
        [SerializeField] private Button homeButton;
        [SerializeField] private Button optionsButton;
        
        

        public override void EnablePanel()
        {
            base.EnablePanel();
            
            TimeManager.Instance.PauseGame();
            
            continueButton.onClick.AddListener(ContinueGame);
            homeButton.onClick.AddListener(ToMainMenu);
            optionsButton.onClick.AddListener(Options);
            
            // testImageButton.gameObject.SetActive(false);
            
            // pauseButton.gameObject.SetActive(false);
            // //pauseButton
            // pauseButton.onClick.RemoveListener(EnablePanel);
        }



        public override void DisablePanel()
        {
            base.DisablePanel();
            
            TimeManager.Instance.UnpauseGame();
            
            continueButton.onClick.RemoveListener(ContinueGame);
            homeButton.onClick.RemoveListener(ToMainMenu);
            optionsButton.onClick.RemoveListener(Options);
            
            // testImageButton.gameObject.SetActive(true);
            
            // pauseButton.gameObject.SetActive(true);
            // //pauseButton
            // pauseButton.onClick.AddListener(EnablePanel);

        }

        private void ContinueGame()
        {
            DisablePanel();
        }
        
        private void ToMainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }

        private void Options()
        {
            DisablePanel();
            settingsPanel.EnablePanel();
            settingsPanel.gameObject.GetComponent<SettingsPanel>().SetPreviousPanel(this);
        }
    }
}
