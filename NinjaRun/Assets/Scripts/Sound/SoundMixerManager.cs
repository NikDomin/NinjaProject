using System;
using UI;
using UnityEngine;
using UnityEngine.Audio;

namespace Sound
{
    public class SoundMixerManager : MonoBehaviour
    {
        [SerializeField] private AudioMixer audioMixer;
        [SerializeField]private SettingsPanel settingsPanel;
        
        private void OnEnable()
        {
            
            settingsPanel.MasterVolumeSlider.onValueChanged.AddListener(SetMasterVolume);
            settingsPanel.SoundFxVolumeSlider.onValueChanged.AddListener(SetSoundFxVolume);
            settingsPanel.MusicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);
        }

        // private void Start()
        // {
        //     SettingsPanel.Instance.MasterVolumeSlider.onValueChanged.AddListener(SetMasterVolume);
        //     SettingsPanel.Instance.SoundFxVolumeSlider.onValueChanged.AddListener(SetSoundFxVolume);
        //     SettingsPanel.Instance.MusicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);
        //
        // }

        private void OnDisable()
        {
            settingsPanel.MasterVolumeSlider.onValueChanged.RemoveListener(SetMasterVolume);
            settingsPanel.SoundFxVolumeSlider.onValueChanged.RemoveListener(SetSoundFxVolume);
            settingsPanel.MusicVolumeSlider.onValueChanged.RemoveListener(SetMusicVolume);
        }

        public void SetMasterVolume(float level)
        {
            audioMixer.SetFloat("masterVolume", Mathf.Log10(level) * 20f );
        }

        public void SetSoundFxVolume(float level)
        {
            audioMixer.SetFloat("SoundFxVolume", Mathf.Log10(level) * 20f);
        }

        public void SetMusicVolume(float level)
        {
            audioMixer.SetFloat("MusicVolume", Mathf.Log10(level) * 20f);
        }
        
    }
}