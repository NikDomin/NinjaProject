using Assets.Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace UI
{
    public class TutorialPanel : Panel
    {
        [SerializeField] private Button acceptButton;
        
        private void OnEnable()
        {
            TimeManager.Instance.PauseGame();
            
            acceptButton.onClick.AddListener(DisablePanel);
        }

        private void OnDisable()
        {
            TimeManager.Instance.UnpauseGame();
            
            acceptButton.onClick.RemoveListener(DisablePanel);

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