using System.Collections;
using Assets.Scripts.Input;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.UI
{
    public class LevelSelector : MonoBehaviour
    {
        [SerializeField] private int level;

        private void Start()
        {
            NewInputManager.PlayerInput.SwitchCurrentActionMap("UI");
        }

        public void LoadScene()
        {
            //SceneManager.LoadScene("Level 1");
            SceneManager.LoadScene("Level " + level.ToString());
        }
    }
}