using System.Collections;
using System.Collections.Generic;
using DataPersistence;
using Services;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class GameOverPanel : Panel
    {
        [SerializeField] private Button homeButton;
        [SerializeField] private Button restartButton;
        [SerializeField] private Button watchAdButton;
        [SerializeField] private Image noAdImage;
        
        public override void EnablePanel()
        {
            base.EnablePanel();

            homeButton.onClick.AddListener(ToMainMenu);
            restartButton.onClick.AddListener(ResetLevel);
            watchAdButton.onClick.AddListener(WatchAd);
            watchAdButton.interactable = true;
            // LevelPlayAds.Instance.OnAdUnavailable += RewardedVideoUnavailable;
            // LevelPlayAds.Instance.OnAdWatchedSuccesfully += AdWatched;
        }
        
        public override void EnablePanelWithDelay(int time)
        {
            base.EnablePanelWithDelay(time);
            
            homeButton.onClick.AddListener(ToMainMenu);
            restartButton.onClick.AddListener(ResetLevel);
            watchAdButton.onClick.AddListener(WatchAd);
            watchAdButton.interactable = true;
            // LevelPlayAds.Instance.OnAdUnavailable += RewardedVideoUnavailable;
            // LevelPlayAds.Instance.OnAdWatchedSuccesfully += AdWatched;

        }

        public override void DisablePanel()
        {
            base.DisablePanel();
            
            homeButton.onClick.RemoveListener(ToMainMenu);
            restartButton.onClick.RemoveListener(ResetLevel);
            watchAdButton.onClick.RemoveListener(WatchAd);
            // LevelPlayAds.Instance.OnAdUnavailable -= RewardedVideoUnavailable;
            // LevelPlayAds.Instance.OnAdWatchedSuccesfully -= AdWatched;
            noAdImage.gameObject.SetActive(false);

        }


        private void ResetLevel()
        {
            StartCoroutine(ResetLevelIEnumerable());
        }
        private void ToMainMenu()
        {
            StartCoroutine(ToMainMenuIEnumerable());
        }


        #region ButtonsListeners

        private IEnumerator ResetLevelIEnumerable()
        {
            DataPersistenceManager.instance.SaveGame();
            yield return new WaitUntil(() => DataPersistenceManager.instance.IsSaved);
            // while (!DataPersistenceManager.instance.IsSaved)
            // {
            //     Debug.Log("Wait");
            // }
            
            DataPersistenceManager.instance.IsSaved = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private IEnumerator ToMainMenuIEnumerable()
        {
            DataPersistenceManager.instance.SaveGame();
            yield return new WaitUntil(() => DataPersistenceManager.instance.IsSaved);
            // while (!DataPersistenceManager.instance.IsSaved)
            // {
            //     Debug.Log("Wait");
            // }
            
            DataPersistenceManager.instance.IsSaved = false;
            SceneManager.LoadScene("MainMenu");
        }
        
        private void WatchAd()
        {
            watchAdButton.interactable = false;
            LevelPlayAds.Instance.ShowRewardedAd();
            DisablePanel();
           
        }


        #endregion

        // private void RewardedVideoUnavailable()
        // {
        //     noAdImage.gameObject.SetActive(true);
        // }
        // private void AdWatched()
        // {
        //     DisablePanel();
        // }
    }
}
