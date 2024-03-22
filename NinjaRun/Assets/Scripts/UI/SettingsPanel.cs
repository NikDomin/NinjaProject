using UnityEngine;
using UnityEngine.UI;
using Utils;


namespace UI
{
    public class SettingsPanel : Panel
    {
        
        
        private Panel previousPanel;
        [Header("Buttons")] 
        [SerializeField] private Button backButton;
        
        
        [field:SerializeField] public Slider MasterVolumeSlider { get; private set; }
        [field:SerializeField] public Slider SoundFxVolumeSlider { get; private set; }
        [field:SerializeField] public Slider MusicVolumeSlider { get; private set; }

        

        public override void EnablePanel()
        {
            base.EnablePanel();
            TimeManager.Instance.PauseGame();
            
            backButton.onClick.AddListener(Back);
        }

        public override void DisablePanel()
        {
            base.DisablePanel();
            
            TimeManager.Instance.UnpauseGame();
            
            backButton.onClick.RemoveListener(Back);
        }

        public void SetPreviousPanel(Panel previousPanel)
        {
            this.previousPanel = previousPanel;
        }

        public void Back()
        {
            DisablePanel();
            previousPanel.EnablePanel();
        }
    }
}