using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Agent.Player;
using DataPersistence;
using Level;
using Level.Resettable;
using Movement;
using Services;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class GameOverPanel : Panel
    {
        public event Action OnEndResetLevel;
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
        }
        
        public override void EnablePanelWithDelay(int time)
        {
            base.EnablePanelWithDelay(time);
            
            homeButton.onClick.AddListener(ToMainMenu);
            restartButton.onClick.AddListener(ResetLevel);
            watchAdButton.onClick.AddListener(WatchAd);
            watchAdButton.interactable = true;
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
            // StartCoroutine(ResetLevelIEnumerable());
            RestartLevel();
        }
        private void ToMainMenu()
        {
            StartCoroutine(ToMainMenuIEnumerable());
        }


        #region ButtonsListeners

        private void RestartLevel()
        {
            // return player to start position
            Transform player = FindObjectOfType<NewSwipeDetection>(true).transform;
            player.position = player.GetComponent<PlayerStartPosition>().StartPosition;
            // //Set player velocity to zero
            // Rigidbody2D playerRigidBogy = player.GetComponent<Rigidbody2D>();
            // playerRigidBogy.gravityScale = 0;
            // playerRigidBogy.velocity = Vector3.zero;
            // playerRigidBogy.angularVelocity = 0;
            
            player.gameObject.SetActive(true);
            player.gameObject.GetComponent<NewSwipeDetection>().ResetAllValue();
            
            var deathWallNewPositionX = player.position.x - 20;
            DeathWall.deathWall.transform.position =
                new Vector3(deathWallNewPositionX, DeathWall.deathWall.transform.position.y);
            DeathWall.deathWall.CanDisableLevelParts = true;
            
            // respawn(or reset) all respawnable item
            var Resettables = FindAllResettableObjects();
            foreach (var resetObject in Resettables)
            {
                resetObject.Reset();
            }
            
            // do something with levelBorder
            
            
            //Action
            OnEndResetLevel?.Invoke();
            
            //Return player gravityScale
            Rigidbody2D playerRigidBogy = player.GetComponent<Rigidbody2D>();
            playerRigidBogy.gravityScale = 1;
            
            //Disable Panel
            DisablePanel();
        }
        private IEnumerator ResetLevelIEnumerable()
        {
            DataPersistenceManager.instance.SaveGame();
            yield return new WaitUntil(() => DataPersistenceManager.instance.IsSaved);
            
            DataPersistenceManager.instance.IsSaved = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private IEnumerator ToMainMenuIEnumerable()
        {
            DataPersistenceManager.instance.SaveGame();
            yield return new WaitUntil(() => DataPersistenceManager.instance.IsSaved);
            
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

        private List<IResettable> FindAllResettableObjects()
        {
            IEnumerable<IResettable> resettables = FindObjectsOfType<MonoBehaviour>(true)
                .OfType<IResettable>();

            return new List<IResettable>(resettables);
        }
        
        
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
