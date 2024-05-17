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
            //virtual 18
            //main 20

            virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
            // virtualCamera = (CinemachineVirtualCamera)FindObjectOfType(typeof(CinemachineVirtualCamera));

            if (PlayerPrefs.GetInt("MainCameraPPU") == 0 || PlayerPrefs.GetFloat("VirtualCameraSize") == 0f)
            {
                if (Screen.width < 1920)
                {
                    Camera.main.GetComponent<PixelPerfectCamera>().assetsPPU = 20;
                    virtualCamera.m_Lens.OrthographicSize = 18f;
                    PlayerPrefs.SetInt("MainCameraPPU", 20);
                    PlayerPrefs.SetFloat("VirtualCameraSize", 18f);
                    return;
                }
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
            if (Screen.width < 1920)
            {
                Camera.main.GetComponent<PixelPerfectCamera>().assetsPPU = 18;
                virtualCamera.m_Lens.OrthographicSize = 20f;
                PlayerPrefs.SetInt("MainCameraPPU", 18);
                PlayerPrefs.SetFloat("VirtualCameraSize", 20f);
                return;
            }
            else
            {
                Camera.main.GetComponent<PixelPerfectCamera>().assetsPPU = 24;
                virtualCamera.m_Lens.OrthographicSize = 22.4f;
                PlayerPrefs.SetInt("MainCameraPPU", 24);
                PlayerPrefs.SetFloat("VirtualCameraSize", 22.4f);
            }
        }

        public void SetMediumCameraSize()
        {
            if (Screen.width < 1920)
            {
                Camera.main.GetComponent<PixelPerfectCamera>().assetsPPU = 20;
                virtualCamera.m_Lens.OrthographicSize = 18f;
                PlayerPrefs.SetInt("MainCameraPPU", 20);
                PlayerPrefs.SetFloat("VirtualCameraSize", 18f);
                return;
            }
            
            Camera.main.GetComponent<PixelPerfectCamera>().assetsPPU = 30;
            virtualCamera.m_Lens.OrthographicSize = 18f;
            PlayerPrefs.SetInt("MainCameraPPU", 30);
            PlayerPrefs.SetFloat("VirtualCameraSize", 18f);
        }

        public void SetSmallCameraSize()
        {
            if (Screen.width < 1920)
            {
                Camera.main.GetComponent<PixelPerfectCamera>().assetsPPU = 23;
                virtualCamera.m_Lens.OrthographicSize = 16f;
                PlayerPrefs.SetInt("MainCameraPPU", 23);
                PlayerPrefs.SetFloat("VirtualCameraSize", 16f);
                return;
            }
            Camera.main.GetComponent<PixelPerfectCamera>().assetsPPU = 35;
            virtualCamera.m_Lens.OrthographicSize = 15.4f;
            PlayerPrefs.SetInt("MainCameraPPU", 35);
            PlayerPrefs.SetFloat("VirtualCameraSize", 15.4f);
        }
    }
}