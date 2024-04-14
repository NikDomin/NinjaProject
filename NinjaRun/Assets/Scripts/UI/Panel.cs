using System.Threading;
using System.Threading.Tasks;
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
            
            if(pauseButton != null)
                pauseButton.gameObject.SetActive(false);
                
        }

        public async virtual void EnablePanelWithDelay(int time)
        {
            try
            {
                await Delay(time);
                NewInputManager.PlayerInput.SwitchCurrentActionMap("UI");
                gameObject.SetActive(true);

                if (pauseButton != null)
                    pauseButton.gameObject.SetActive(false);
            }
            catch
            {
                NewInputManager.PlayerInput.SwitchCurrentActionMap("UI");
                
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
            
            NewInputManager.PlayerInput.SwitchCurrentActionMap("Touch");
            
            
            //Fix stupid BUG when I cant press the pause button
            NewInputManager.PlayerInput.actions.Enable();

            gameObject.SetActive(false);
            
            if(pauseButton != null)
                pauseButton.gameObject.SetActive(true);
        }
    }
}