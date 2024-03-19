using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utils;

namespace UI
{
    public class PausePanel : Panel
    {
        [SerializeField] private Image optionsPanel;
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
            
        }



        public override void DisablePanel()
        {
            base.DisablePanel();
            
            TimeManager.Instance.UnpauseGame();

            continueButton.onClick.RemoveListener(ContinueGame);
            homeButton.onClick.RemoveListener(ToMainMenu);
            optionsButton.onClick.RemoveListener(Options);
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
            optionsButton.gameObject.SetActive(true);
        }
    }
}
