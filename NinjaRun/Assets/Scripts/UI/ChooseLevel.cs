using System;
using System.Collections.Generic;
using System.Linq;
using DataPersistence;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
    public class ChooseLevel : MonoBehaviour
    {
        [SerializeField] private ChooseLevelButtons[] buttonsArray;
        [SerializeField] private Button rightButton, leftButton;
        private int currentActiveButtonsArray;
        private DataPersistenceManager dataPersistenceManager;

        private void Awake()
        {
            Debug.Log(buttonsArray.Length);
            
            if(currentActiveButtonsArray == 0)
                leftButton.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            rightButton.onClick.AddListener(ToTheRight);
            leftButton.onClick.AddListener(ToTheLeft);
        }
        private void OnDisable()
        {
            rightButton.onClick.RemoveListener(ToTheRight);
            leftButton.onClick.RemoveListener(ToTheLeft);
        
        }


        private void ToTheLeft()
        {
            currentActiveButtonsArray--;
            rightButton.gameObject.SetActive(true);
            if(currentActiveButtonsArray == 0)
                leftButton.gameObject.SetActive(false);
            DisableAllArrays();
            buttonsArray[currentActiveButtonsArray].EnableButtons();
        }

        
        
        private void ToTheRight()
        {
            currentActiveButtonsArray++;
            leftButton.gameObject.SetActive(true);
            if(currentActiveButtonsArray == buttonsArray.Length-1)
                rightButton.gameObject.SetActive(false);
            DisableAllArrays();
            buttonsArray[currentActiveButtonsArray].EnableButtons();
        }

        private void DisableAllArrays()
        {
            foreach (var array in buttonsArray)
            {
                array.DisableButtons();
            }
        }

        public void FindLevelNeedToPass()
        {
            int levelNeedToPass;
            List <LevelSelector> completedLevels = GetCompletedLevels();
            if (completedLevels == null || completedLevels.Count == 0)
            {
                // set first level need to pass;
                levelNeedToPass = 1;
            }
            else
            {
                levelNeedToPass = completedLevels.Max(level => level.LevelName);
                levelNeedToPass++;
            }
            var allLevels = GetAllLevels();
            if (allLevels == null)
            { 
                Debug.LogError("Cant find levels");
                return;
            }
            if (levelNeedToPass > allLevels.Count)
                return;
                
            LevelSelector LevelSelectorNeedToPass = allLevels.FirstOrDefault(level => level.LevelName == levelNeedToPass);
            LevelSelectorNeedToPass.SetLevelNeedToComplete();
        }

        private List<LevelSelector> GetCompletedLevels()
        {
            List<LevelSelector> completedLevels = new List<LevelSelector>();
            foreach (var buttonArray in buttonsArray)
            {
                foreach (var levelSelector in buttonArray.buttonLevelSelector)
                {
                    if (levelSelector.IsLevelPassed)
                        completedLevels.Add(levelSelector);
                    
                }
            }
            return completedLevels;
        }

        private List<LevelSelector> GetAllLevels()
        {
            List<LevelSelector> allLevels = new List<LevelSelector>();
            foreach (var buttonArray in buttonsArray)
            {
                foreach (var level in buttonArray.buttonLevelSelector)
                {
                    allLevels.Add(level);
                }
            }

            return allLevels;
        }
    }

    [Serializable]
    public class ChooseLevelButtons
    {
        [field: SerializeField] public LevelSelector[] buttonLevelSelector;

        public void DisableButtons()
        {
            foreach (var button in buttonLevelSelector)
            {
                button.gameObject.SetActive(false);
            }
        }

        public void EnableButtons()
        {
            foreach (var button in buttonLevelSelector)
            {
                button.gameObject.SetActive(true);
            }
        }
    }
}