using Cinemachine;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

namespace UI
{
    public class CameraSizeSettings : MonoBehaviour
    {
        [SerializeField] private bool isChangeSizeOnThisScene;
        [SerializeField] private Button smallSize, mediumSize, bigSize;
        private CinemachineVirtualCamera virtualCamera;
        
        private void Awake()
        {
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
            Camera.main.GetComponent<PixelPerfectCamera>().assetsPPU = 24;
            virtualCamera.m_Lens.OrthographicSize = 22.4f;
            PlayerPrefs.SetInt("MainCameraPPU", 24);
            PlayerPrefs.SetFloat("VirtualCameraSize", 22.4f);
        }

        private void SetMediumCameraSize()
        {
            Camera.main.GetComponent<PixelPerfectCamera>().assetsPPU = 30;
            virtualCamera.m_Lens.OrthographicSize = 18f;
            PlayerPrefs.SetInt("MainCameraPPU", 30);
            PlayerPrefs.SetFloat("VirtualCameraSize", 18f);
        }

        private void SetSmallCameraSize()
        {
            Camera.main.GetComponent<PixelPerfectCamera>().assetsPPU = 35;
            virtualCamera.m_Lens.OrthographicSize = 15.4f;
            PlayerPrefs.SetInt("MainCameraPPU", 35);
            PlayerPrefs.SetFloat("VirtualCameraSize", 15.4f);
        }
    }
}