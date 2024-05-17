using System;
using DataPersistence;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;
using UnityEngine.UI;

namespace Services
{
    public class Authentication : MonoBehaviour
    {
        public event Action OnSuccesAuthentication;
        public event Action OnFailedAuthentication;
        [SerializeField] private Image errorAutomaticAuthentication;
        [Header("Menu UI")] 
        [SerializeField] private Image menuImage;
        [SerializeField] private Button settingsButton;
        [SerializeField] private Button achievementButton;
        [SerializeField] private Button leaderBoardButton;

        private DataPersistenceManager dataPersistenceManager;

        private void Awake()
        {
            settingsButton.interactable = false;
            menuImage.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            
            dataPersistenceManager = FindObjectOfType<DataPersistenceManager>();
            Debug.Log("Authentication subscribe to OnLoadEndSuccesfully with DataPersistenceManger: " + dataPersistenceManager.name);
            dataPersistenceManager.OnLoadEndSuccefully += LoadSuccesfully;
        }

        private void OnDisable()
        {
           dataPersistenceManager.OnLoadEndSuccefully -= LoadSuccesfully;
        }

        private void LoadSuccesfully()
        {
            Debug.Log("LoadSuccesfully");
            settingsButton.interactable = true;
            menuImage.gameObject.SetActive(true);
        }

        private void Start()
        {
            if (!Social.localUser.authenticated)
            {
                PlayGamesPlatform.DebugLogEnabled = true;
                PlayGamesPlatform.Activate();
                PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
            }
            
        }
        internal void ProcessAuthentication(SignInStatus status)
        {
            if (status == SignInStatus.Success)
            {
                // Continue with Play Games Services
                OnSuccesAuthentication?.Invoke();
                // string name = PlayGamesPlatform.Instance.GetUserDisplayName();
            }
            else
            {
                Debug.LogError("Error during automatic authentication in PlayGamePlatform");
                errorAutomaticAuthentication.gameObject.SetActive(true);
                menuImage.gameObject.SetActive(false);
                settingsButton.gameObject.SetActive(false);
                // Disable your integration with Play Games Services or show a login button
                // to ask users to sign-in. Clicking it should call
                // PlayGamesPlatform.Instance.ManuallyAuthenticate(ProcessAuthentication).
            }
        }

        public void ManuallyAuthenticate()
        {
            PlayGamesPlatform.Instance.ManuallyAuthenticate(ProcessManuallyAuthentication);
            errorAutomaticAuthentication.gameObject.SetActive(false);
            menuImage.gameObject.SetActive(true);
            settingsButton.gameObject.SetActive(true);
        }

        private void ProcessManuallyAuthentication(SignInStatus status)
        {
            if (status == SignInStatus.Success)
            {
                OnSuccesAuthentication?.Invoke();
            }
            else
            {
                OnFailedAuthentication?.Invoke();
                Debug.LogError("Error during manual authentication in PlayGamePlatform");
            }
        }
    }
}