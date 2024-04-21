using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class CameraSizeUI : MonoBehaviour
    {
        [SerializeField] private Button smallSize, mediumSize, bigSize;
        
        private void OnEnable()
        {
            smallSize.onClick.AddListener(SetSmallCameraSize);
            mediumSize.onClick.AddListener(SetMediumCameraSize);
            bigSize.onClick.AddListener(SetBigCameraSize);
        }
        private void OnDisable()
        {
            smallSize.onClick.RemoveListener(SetSmallCameraSize);
            mediumSize.onClick.RemoveListener(SetMediumCameraSize);
            bigSize.onClick.RemoveListener(SetBigCameraSize);
        }
        
        private void SetBigCameraSize()
        {
            CameraSizeSettings.Instance.SetBigCameraSize();
        }

        private void SetMediumCameraSize()
        {
            CameraSizeSettings.Instance.SetMediumCameraSize();
        }

        private void SetSmallCameraSize()
        {
            CameraSizeSettings.Instance.SetSmallCameraSize(); 
        }
        
    }
}