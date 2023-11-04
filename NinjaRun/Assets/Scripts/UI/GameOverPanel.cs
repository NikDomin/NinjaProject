using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.UI
{
    public class GameOverPanel : Panel
    {
        public override void EnablePanel()
        {
            base.EnablePanel();
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
    }
}