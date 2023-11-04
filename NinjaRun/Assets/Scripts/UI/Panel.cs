using System.Collections;
using Assets.Scripts.Input;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public abstract class Panel : MonoBehaviour
    {
        public virtual void EnablePanel()
        {
            NewInputManager.PlayerInput.SwitchCurrentActionMap("UI");
            gameObject.SetActive(true);
        }

        public virtual void DisablePanel()
        {
            NewInputManager.PlayerInput.SwitchCurrentActionMap("Touch");
            gameObject.SetActive(false);
        }
    }
}