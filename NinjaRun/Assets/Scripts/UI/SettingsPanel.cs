using DataPersistence;
using DataPersistence.Data;
using UnityEngine;
using UnityEngine.UI;
using Utils;


namespace UI
{
    public class SettingsPanel : Panel, IDataPersistence
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
            
            if(TimeManager.Instance != null) 
                TimeManager.Instance.PauseGame();
            
            backButton.onClick.AddListener(Back);
        }

        public override void DisablePanel()
        {
            base.DisablePanel();
            
            if(TimeManager.Instance != null) 
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
            
            if(previousPanel != null)
                previousPanel.EnablePanel();
        }


        public void LoadData(GameData data)
        {
            MasterVolumeSlider.value = data.MasterVolume;        
            SoundFxVolumeSlider.value = data.SFXVolume;
            MusicVolumeSlider.value = data.MusicVolume;
        }

        public void SaveData(GameData data)
        {
            data.MasterVolume = MasterVolumeSlider.value;
            data.SFXVolume = SoundFxVolumeSlider.value;
            data.MusicVolume = MusicVolumeSlider.value;
            
        }
    }
}