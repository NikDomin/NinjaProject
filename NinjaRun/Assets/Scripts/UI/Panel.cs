using Input;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public abstract class Panel : MonoBehaviour
    {
        [SerializeField] protected Button pauseButton;
        
        public virtual void EnablePanel()
        {
            NewInputManager.PlayerInput.SwitchCurrentActionMap("UI");
            
            gameObject.SetActive(true);
            pauseButton.gameObject.SetActive(false);
        }

        public virtual void DisablePanel()
        {
            
            NewInputManager.PlayerInput.SwitchCurrentActionMap("Touch");
            
            
            //Fix stupid BUG when I cant press the pause button
            NewInputManager.PlayerInput.actions.Enable();

            gameObject.SetActive(false); 
            pauseButton.gameObject.SetActive(true);

        }
    }
}