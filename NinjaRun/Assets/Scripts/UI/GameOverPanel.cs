using System.Collections;
using Assets.Scripts.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.UI
{
    public class GameOverPanel : Panel
    {
        public override void EnablePanel()
        {
            base.EnablePanel();
            GameUtils.SceneNumber(SceneManager.GetActiveScene());
        }

        public override void DisablePanel()
        {
            base.DisablePanel();
        }

        public void ResetLevel()
        {
            DisablePanel();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void ToMainMenu()
        {
            SceneManager.LoadScene("ChooseLevel");
        }
    }
}