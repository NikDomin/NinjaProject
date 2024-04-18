using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ChooseLevel : MonoBehaviour
    {
        [SerializeField] private ChooseLevelButtons[] buttonsArray;
        [SerializeField] private Button rightButton, leftButton;
        private int currentActiveButtonsArray;

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
    }

    [Serializable]
    public class ChooseLevelButtons
    {
        [field: SerializeField] public Button[] buttons;

        public void DisableButtons()
        {
            foreach (var button in buttons)
            {
                button.gameObject.SetActive(false);
            }
        }

        public void EnableButtons()
        {
            foreach (var button in buttons)
            {
                button.gameObject.SetActive(true);
            }
        }
    }
}