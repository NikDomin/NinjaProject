using Input.Old_Input;
using UI;
using UnityEngine;

namespace Level.ESCButton
{
    public class ToMainMenu : MonoBehaviour
    {
        private SceneSelector sceneSelector;
        private OldInputManager inputManager;

        private void Awake()
        {
            sceneSelector = GetComponent<SceneSelector>();
            inputManager = OldInputManager.Instance;
        }

        private void OnEnable()
        {
            inputManager.OnEscapeClicked.AddListener(ToMenu);
        }

        private void OnDisable()
        {
            inputManager.OnEscapeClicked.RemoveListener(ToMenu);
        }

        private void ToMenu()
        {
            sceneSelector.LoadScene();
        }
    }
}