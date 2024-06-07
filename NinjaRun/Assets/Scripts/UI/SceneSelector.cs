using System.Collections;
using Input.Old_Input.Types;
using DataPersistence;
using Input.Old_Input;
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
            OldInputManager.Instance.ChangeActionMap(ActionMaps.UI);

          
            
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
            StartCoroutine(LoadWithSave());
        }

        private IEnumerator LoadWithSave()
        {
            DataPersistenceManager.instance.SaveGame();
            yield return new WaitUntil(() => DataPersistenceManager.instance.IsSaved);
            // while (!DataPersistenceManager.instance.IsSaved)
            // {
            //     Debug.Log("Wait");
            // }
            
            DataPersistenceManager.instance.IsSaved = false;
            SceneManager.LoadScene(levelName);
        }
    }
}