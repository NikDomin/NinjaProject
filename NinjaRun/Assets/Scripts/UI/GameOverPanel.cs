using System.Collections;
using System.Collections.Generic;
using DataPersistence;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class GameOverPanel : Panel
    {
        [SerializeField] private Button homeButton;
        [SerializeField] private Button restartButton;
        
        public override void EnablePanel()
        {
            base.EnablePanel();

            homeButton.onClick.AddListener(ToMainMenu);
            restartButton.onClick.AddListener(ResetLevel);
        }


        public override void EnablePanelWithDelay(int time)
        {
            base.EnablePanelWithDelay(time);
            
            homeButton.onClick.AddListener(ToMainMenu);
            restartButton.onClick.AddListener(ResetLevel);
        }

        public override void DisablePanel()
        {
            base.DisablePanel();
            
            homeButton.onClick.RemoveListener(ToMainMenu);
            restartButton.onClick.RemoveListener(ResetLevel);
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

        #endregion
    }
}