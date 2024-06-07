using System.Threading.Tasks;
using Input.Old_Input.Types;
using Input;
using Input.Old_Input;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public abstract class Panel : MonoBehaviour
    {
        [SerializeField] protected Button pauseButton;
        
        public virtual void EnablePanel()
        {
            // NewInputManager.PlayerInput.SwitchCurrentActionMap("UI");
            OldInputManager.Instance.ChangeActionMap(ActionMaps.UI);
            
            gameObject.SetActive(true);
            
            if(pauseButton != null)
                pauseButton.gameObject.SetActive(false);
                
        }

        public async virtual void EnablePanelWithDelay(int time)
        {
            try
            {
                await Delay(time); 
                OldInputManager.Instance.ChangeActionMap(ActionMaps.UI);
                gameObject.SetActive(true);

                if (pauseButton != null)
                    pauseButton.gameObject.SetActive(false);
            }
            catch
            {
                OldInputManager.Instance.ChangeActionMap(ActionMaps.UI);

                gameObject.SetActive(true);
                
                if(pauseButton != null)
                    pauseButton.gameObject.SetActive(false);
            }
            
            
        }

        private async Task Delay(int time)
        {
            await Task.Delay(time, NewInputManager.Instance.Token);
        }

        public virtual void DisablePanel()
        {
            
            OldInputManager.Instance.ChangeActionMap(ActionMaps.Touch);

            
            //Fix stupid BUG when I cant press the pause button
            // NewInputManager.PlayerInput.actions.Enable();

            gameObject.SetActive(false);
            
            if(pauseButton != null)
                pauseButton.gameObject.SetActive(true);
        }
    }
}