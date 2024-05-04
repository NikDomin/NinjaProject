using System;
using System.Collections.Generic;
using System.Linq;
using Agent.Player.PlayerStateMachine;
using Level;
using TMPro;
using UI;
using UnityEngine;

namespace Services
{
    public class LevelPlayAds : MonoBehaviour
    {

        public event Action OnAdUnavailable;
        public event Action OnAdWatchedSuccesfully;
        public static LevelPlayAds Instance { get; private set; }
        
        #region Mono

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("Found more than one LevelPlayAds in the scene. Destroying the newest one.");
                Destroy(gameObject);
                
                return;
            }
            
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void OnEnable()
        {
            IronSourceEvents.onSdkInitializationCompletedEvent += SdkInitializationCompletedEvent;
            
            //Add AdInfo Rewarded Video Events
            IronSourceRewardedVideoEvents.onAdOpenedEvent += RewardedVideoOnAdOpenedEvent;
            IronSourceRewardedVideoEvents.onAdClosedEvent += RewardedVideoOnAdClosedEvent;
            IronSourceRewardedVideoEvents.onAdAvailableEvent += RewardedVideoOnAdAvailable;
            IronSourceRewardedVideoEvents.onAdUnavailableEvent += RewardedVideoOnAdUnavailable;
            IronSourceRewardedVideoEvents.onAdShowFailedEvent += RewardedVideoOnAdShowFailedEvent;
            IronSourceRewardedVideoEvents.onAdRewardedEvent += RewardedVideoOnAdRewardedEvent;
            IronSourceRewardedVideoEvents.onAdClickedEvent += RewardedVideoOnAdClickedEvent;
        }

        private void OnDisable()
        {
            IronSourceEvents.onSdkInitializationCompletedEvent -= SdkInitializationCompletedEvent;
            
            //Add AdInfo Rewarded Video Events
            IronSourceRewardedVideoEvents.onAdOpenedEvent -= RewardedVideoOnAdOpenedEvent;
            IronSourceRewardedVideoEvents.onAdClosedEvent -= RewardedVideoOnAdClosedEvent;
            IronSourceRewardedVideoEvents.onAdAvailableEvent -= RewardedVideoOnAdAvailable;
            IronSourceRewardedVideoEvents.onAdUnavailableEvent -= RewardedVideoOnAdUnavailable;
            IronSourceRewardedVideoEvents.onAdShowFailedEvent -= RewardedVideoOnAdShowFailedEvent;
            IronSourceRewardedVideoEvents.onAdRewardedEvent -= RewardedVideoOnAdRewardedEvent;
            IronSourceRewardedVideoEvents.onAdClickedEvent -= RewardedVideoOnAdClickedEvent;
        }

        private void Start()
        {
            // GameOverPanel gameOverPanel = FindObjectOfType<GameOverPanel>();
            // Debug.Log("GAME OVER PANEL: " + gameOverPanel);
            Debug.Log("LEVEL PLAY ADS START");
            IronSource.Agent.setMetaData("is_deviceid_optout","true");
            IronSource.Agent.setMetaData("is_child_directed","true");
            IronSource.Agent.init ("1e5fbc715");
            IronSource.Agent.validateIntegration();
        }


        
        void OnApplicationPause(bool isPaused) {                 
            IronSource.Agent.onApplicationPause(isPaused);
        }

        #endregion
        
        public void ShowRewardedAd()
        {
            if (IronSource.Agent.isRewardedVideoAvailable())
            {
                IronSource.Agent.showRewardedVideo();
            }
            else
            {
                Debug.Log("Reward video not available");
            }
        }

        private void SdkInitializationCompletedEvent(){}


        #region RewardAD

/************* RewardedVideo AdInfo Delegates *************/
// Indicates that there’s an available ad.
// The adInfo object includes information about the ad that was loaded successfully
// This replaces the RewardedVideoAvailabilityChangedEvent(true) event
        void RewardedVideoOnAdAvailable(IronSourceAdInfo adInfo) {
        }
// Indicates that no ads are available to be displayed
// This replaces the RewardedVideoAvailabilityChangedEvent(false) event
        void RewardedVideoOnAdUnavailable()
        {
            OnAdUnavailable?.Invoke();   
        }
// The Rewarded Video ad view has opened. Your activity will loose focus.
        void RewardedVideoOnAdOpenedEvent(IronSourceAdInfo adInfo){
        }
// The Rewarded Video ad view is about to be closed. Your activity will regain its focus.
        void RewardedVideoOnAdClosedEvent(IronSourceAdInfo adInfo){
        }
// The user completed to watch the video, and should be rewarded.
// The placement parameter will include the reward data.
// When using server-to-server callbacks, you may ignore this event and wait for the ironSource server callback.
        void RewardedVideoOnAdRewardedEvent(IronSourcePlacement placement, IronSourceAdInfo adInfo){
            // //reward user
            var player = FindObjectOfType<PlayerState>(true).gameObject;
            var checkPoints = FindAllActiveCheckPoints();
            var firstCheckPoint = checkPoints
                .Where(checkpoint => checkpoint.transform.position.x < player.transform.position.x)
                .OrderByDescending(checkPoint => checkPoint.transform.position.x)
                .First();
            // CheckPoint checkPoint = checkPointsActivatedByPlayer.Max();
            firstCheckPoint.SpawnHero(player);
            OnAdWatchedSuccesfully?.Invoke();
            
        }
// The rewarded video ad was failed to show.
        void RewardedVideoOnAdShowFailedEvent(IronSourceError error, IronSourceAdInfo adInfo){
        }
// Invoked when the video ad was clicked.
// This callback is not supported by all networks, and we recommend using it only if
// it’s supported by all networks you included in your build.
        void RewardedVideoOnAdClickedEvent(IronSourcePlacement placement, IronSourceAdInfo adInfo){
        }
        

        #endregion

        private List<CheckPoint> FindAllActiveCheckPoints()
        {
            var checkPoints = FindObjectsOfType<CheckPoint>();

            return new List<CheckPoint>(checkPoints);
        }
    }
}