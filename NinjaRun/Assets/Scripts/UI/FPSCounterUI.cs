using System;
using Level;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class FPSCounterUI : MonoBehaviour
    {
        [SerializeField] private Button FPS30Button, FPS60Button;

        private void OnEnable()
        {
            FPS30Button.onClick.AddListener(Set30FPS);
            FPS60Button.onClick.AddListener(Set60FPS);
        }

        private void OnDisable()
        {
            FPS30Button.onClick.RemoveListener(Set30FPS);
            FPS60Button.onClick.RemoveListener(Set60FPS);

        }


        private void Set30FPS()
        {
            var FPSManager = FindObjectOfType<FrameRateManager>();
            if (FPSManager == null)
            {
                Debug.LogError("FPS Manager is null");
                return;
            }
            FPSManager.Set30FPS();
        }
        
        private void Set60FPS()
        {
            var FPSManager = FindObjectOfType<FrameRateManager>();
            if (FPSManager == null)
            {
                Debug.LogError("FPS Manager is null");
                return;
            }
            FPSManager.Set60FPS();
        }
    }   
}