using System.Collections;
using DataPersistence;
using DataPersistence.Data;
using Input;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
    public class LevelSelector : MonoBehaviour, IDataPersistence, ILevelSelector
    {
        [FormerlySerializedAs("level")] [SerializeField] private string levelName;
        [SerializeField] private bool testIsLevelPassed;
        [SerializeField] private Image lockImage;

     
        
        private void Start()
        {
            NewInputManager.PlayerInput.SwitchCurrentActionMap("UI");
            
            GetComponentInChildren<TextMeshProUGUI>().text = levelName;
        }

        private void OnValidate()
        {
           
            GetComponentInChildren<TextMeshProUGUI>().text = levelName;
        }
        
        public void LoadScene()
        {
            StartCoroutine(LoadSceneWithSave());
        }
        private IEnumerator LoadSceneWithSave()
        {
            //Save Game
            DataPersistenceManager.instance.SaveGame();
            yield return new WaitUntil(() => DataPersistenceManager.instance.IsSaved);
            // while (!DataPersistenceManager.instance.IsSaved)
            // {
            //     Debug.Log("Wait");
            // }
            
            DataPersistenceManager.instance.IsSaved = false;
            SceneManager.LoadScene("Level " + levelName);
        }

        private void SetImageAlpha(float alpha)
        {
            Image image = GetComponent<Image>();
            var imageColor = image.color;
            imageColor.a = alpha;
            image.color = imageColor;
        }

        #region SaveSystem

        public void LoadData(GameData data)
        {
            data.LevelPassed.TryGetValue(levelName, out testIsLevelPassed);
            if (testIsLevelPassed)
            {
                lockImage.gameObject.SetActive(false);
                GetComponent<Button>().interactable = true;
            }
            else if (!testIsLevelPassed && levelName == data.levelNeedToPass)
            {
                SetImageAlpha(0.5f);
                lockImage.gameObject.SetActive(false);
                GetComponent<Button>().interactable = true;
            }
            else if (!testIsLevelPassed)
            {
                SetImageAlpha(0.5f);
                lockImage.gameObject.SetActive(true);
                GetComponent<Button>().interactable = false;
            }
            // else if(!testIsLevelPassed && )
        }

        public void SaveData(GameData data)
        {
            
        }

        #endregion
    }
}