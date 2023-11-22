using System.Collections;
using Assets.Scripts.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.UI
{
    public class CompleteLevel : Panel
    {

        private void OnEnable()
        {
            TimeManager.Instance.PauseGame();
        }
        private void OnDisable()
        {
            TimeManager.Instance.UnpauseGame();
        }

        public override void EnablePanel()
        {
            base.EnablePanel();
        }

        public override void DisablePanel()
        {
            base.DisablePanel();
        }

        public void ToMainMenu()
        {
            SceneManager.LoadScene("ChooseLevel");
        }

        public void ToNextLevel()
        {
            SceneManager.LoadScene("Level " + (GameUtils.SceneNumber(SceneManager.GetActiveScene())+1));
        }
    }
}