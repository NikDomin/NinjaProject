using DataPersistence;
using Input;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class SceneSelector :MonoBehaviour, ILevelSelector
    {
        [SerializeField] private string levelName;

        [Header("For editor only")] 
        [SerializeField]
        private bool isHasTextInChildren = true;
        
        private void Start()
        {
            NewInputManager.PlayerInput.SwitchCurrentActionMap("UI");
          
            
            if(isHasTextInChildren)
                GetComponentInChildren<TextMeshProUGUI>().text = levelName;
        }
        private void OnValidate()
        {
            if(isHasTextInChildren)
                GetComponentInChildren<TextMeshProUGUI>().text = levelName;
        }
        

        public void LoadScene()
        {
            DataPersistenceManager.instance.SaveGame();
            SceneManager.LoadScene(levelName);
        }
    }
}