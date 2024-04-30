using System;
using System.Collections;
using System.Collections.Generic;
using DataPersistence;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utils;

namespace UI
{
    public class CompleteLevel : Panel
    {
        [SerializeField] private Button homeButton;
        [SerializeField] private Button nextLevelButton;
        

        private void OnEnable()
        {
            TimeManager.Instance.PauseGame();
            
            homeButton.onClick.AddListener(ToMainMenu);
            nextLevelButton.onClick.AddListener(ToNextLevel);
        }
        private void OnDisable()
        {
            TimeManager.Instance.UnpauseGame();
            
            homeButton.onClick.RemoveListener(ToMainMenu);
            nextLevelButton.onClick.RemoveListener(ToNextLevel);

        }

        private void ToNextLevel()
        {
            StartCoroutine(toNextLevelIEnumerable());
        }


        public override void EnablePanel()
        {
            base.EnablePanel();
        }

        public override void DisablePanel()
        {
            base.DisablePanel();
        }

        #region ButtonsListeners

        private void ToMainMenu()
        {
            StartCoroutine(toMainMenuIEnumerable());
        }

        private IEnumerator toMainMenuIEnumerable()
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

        private IEnumerator toNextLevelIEnumerable()
        {
            DataPersistenceManager.instance.SaveGame();
            yield return new WaitUntil(() => DataPersistenceManager.instance.IsSaved);
            // while (!DataPersistenceManager.instance.IsSaved)
            // {
            //     Debug.Log("Wait");
            // }
            
            DataPersistenceManager.instance.IsSaved = false;
            SceneManager.LoadScene("Level " + (GameUtils.SceneNumber(SceneManager.GetActiveScene())+1));
        }

        #endregion
    }
}