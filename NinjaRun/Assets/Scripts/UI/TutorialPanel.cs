using System.Collections;
using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class TutorialPanel : Panel
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
    }
}