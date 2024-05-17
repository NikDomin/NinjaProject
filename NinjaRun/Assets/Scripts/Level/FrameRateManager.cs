using System;
using UnityEngine;

namespace Level
{
    public class FrameRateManager : MonoBehaviour
    {
        public static FrameRateManager Instance;
        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
            if (PlayerPrefs.GetInt("FPS") == 0 || PlayerPrefs.GetInt("FPS") == 30)
            {
                QualitySettings.vSyncCount = 1;
                PlayerPrefs.SetInt("FPS", 30);
            }
            else
            {
                QualitySettings.vSyncCount = 0;
                Application.targetFrameRate = PlayerPrefs.GetInt("FPS");
            }
        }

        public void Set30FPS()
        {
            QualitySettings.vSyncCount = 1;
            PlayerPrefs.SetInt("FPS", 30);
        }

        public void Set60FPS()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 60;
            PlayerPrefs.SetInt("FPS", 60);
        }
    }
}