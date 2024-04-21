using Cinemachine;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

namespace UI
{
    public class CameraSizeSettings : MonoBehaviour
    {
        public static CameraSizeSettings Instance;
        [SerializeField] private bool isChangeSizeOnThisScene;
       
        private CinemachineVirtualCamera virtualCamera;
        
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            
            if (!isChangeSizeOnThisScene)
            {
                gameObject.SetActive(false);
                return;
            }
            
            virtualCamera = (CinemachineVirtualCamera)FindObjectOfType(typeof(CinemachineVirtualCamera));

            if (PlayerPrefs.GetInt("MainCameraPPU") == 0 || PlayerPrefs.GetFloat("VirtualCameraSize") == 0f)
            {
                Camera.main.GetComponent<PixelPerfectCamera>().assetsPPU = 30;
                virtualCamera.m_Lens.OrthographicSize = 18f;
                PlayerPrefs.SetInt("MainCameraPPU", 30);
                PlayerPrefs.SetFloat("VirtualCameraSize", 18f);
            }
            else
            {
                Camera.main.GetComponent<PixelPerfectCamera>().assetsPPU = PlayerPrefs.GetInt("MainCameraPPU");
                virtualCamera.m_Lens.OrthographicSize = PlayerPrefs.GetFloat("VirtualCameraSize");
            }
            
        }
        public void SetBigCameraSize()
        {
            Camera.main.GetComponent<PixelPerfectCamera>().assetsPPU = 24;
            virtualCamera.m_Lens.OrthographicSize = 22.4f;
            PlayerPrefs.SetInt("MainCameraPPU", 24);
            PlayerPrefs.SetFloat("VirtualCameraSize", 22.4f);
        }

        public void SetMediumCameraSize()
        {
            Camera.main.GetComponent<PixelPerfectCamera>().assetsPPU = 30;
            virtualCamera.m_Lens.OrthographicSize = 18f;
            PlayerPrefs.SetInt("MainCameraPPU", 30);
            PlayerPrefs.SetFloat("VirtualCameraSize", 18f);
        }

        public void SetSmallCameraSize()
        {
            Camera.main.GetComponent<PixelPerfectCamera>().assetsPPU = 35;
            virtualCamera.m_Lens.OrthographicSize = 15.4f;
            PlayerPrefs.SetInt("MainCameraPPU", 35);
            PlayerPrefs.SetFloat("VirtualCameraSize", 15.4f);
        }
    }
}