using Input.Old_Input;
using UI;
using UnityEngine;

namespace Level.ESCButton
{
    public class PauseGame : MonoBehaviour
    {
        [SerializeField] private Panel PausePanel;
        private OldInputManager inputManager;

        private void Awake()
        {
            inputManager = OldInputManager.Instance;
            
        }

        private void OnEnable()
        {
            inputManager.OnEscapeClicked.AddListener(Pause);
        }

        private void OnDisable()
        {
            inputManager.OnEscapeClicked.RemoveListener(Pause);
        }

        private void Pause()
        {
            PausePanel.EnablePanel();
        }
    }
}