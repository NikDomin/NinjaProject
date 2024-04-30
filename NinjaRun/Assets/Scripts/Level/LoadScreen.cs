using UnityEngine;

namespace Level
{
    public class LoadScreen : MonoBehaviour
    {
        [SerializeField] private GameObject loadScreen;

        private void Awake()
        {
            ShowLoadScreen();
        }

        public void ShowLoadScreen() =>loadScreen.gameObject.SetActive(true);

        public void HideLoadScreen()
        {
            if(loadScreen != null) 
                loadScreen.gameObject.SetActive(false);
        }
            
    }
}