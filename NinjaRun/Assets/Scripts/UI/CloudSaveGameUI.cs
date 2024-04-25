using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
    public class CloudSaveGameUI : MonoBehaviour
    { 
        public string MyName;
        public int Age;
        public Text LogText;
        public Text OutputText;
        public TMP_InputField NameInputField;
        public TMP_InputField AgeInputField;


        private void OnEnable()
        {
            NameInputField.onValueChanged.AddListener(OnValueChangeName); 
            AgeInputField.onValueChanged.AddListener(OnValueChangeAge);
        }

        private void OnDisable()
        {
            NameInputField.onValueChanged.RemoveListener(OnValueChangeName); 
            AgeInputField.onValueChanged.RemoveListener(OnValueChangeAge);

        }

        public void OnValueChangeName(string field)
        {
            MyName = NameInputField.text;
            
        }
        public void OnValueChangeAge(string field)
        {
            Age = int.Parse(AgeInputField.text);
        }
    }
}