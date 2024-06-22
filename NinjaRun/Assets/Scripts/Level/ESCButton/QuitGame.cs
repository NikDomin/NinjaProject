using Input.Old_Input;
using UnityEngine;

namespace Level.ESCButton
{
    public class QuitGame : MonoBehaviour
    {
        private OldInputManager inputManager;

        private void Awake()
        {
            inputManager = OldInputManager.Instance;
        }

        private void OnEnable()
        {
            inputManager.OnEscapeClicked.AddListener(Quit); 
        }

        private void OnDisable()
        {
            inputManager.OnEscapeClicked.RemoveListener(Quit);
        }

        private void Quit()
        {
            AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
            activity.Call<bool>("moveTaskToBack", true);
        }
    }
}