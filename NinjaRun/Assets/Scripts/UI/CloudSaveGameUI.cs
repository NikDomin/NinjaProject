using System;
using System.Collections;
using System.Collections.Generic;
using DataPersistence;
using DataPersistence.Data;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
    public class CloudSaveGameUI : MonoBehaviour
    { 
        //
        // public static CloudSaveGameUI Instance;
        // public Text LogText;
        // public Text OutputText;
        // public Text JsonText;
        // public Text TestHandlerText;
        // public Button SaveButton;
        // public Button LoadButton;
        //
        // private void Awake()
        // {
        //     if (Instance == null)
        //         Instance = this;
        // }
        //
        // private void OnEnable()
        // {
        //     SaveButton.onClick.AddListener(Save);
        //     LoadButton.onClick.AddListener(Load);
        // }
        //
        //
        // private void OnDisable()
        // {
        //     SaveButton.onClick.RemoveListener(Save);
        //     LoadButton.onClick.RemoveListener(Load);
        // }
        //
        // private void Save()
        // {
        //     StartCoroutine(SaveIEnumerable());
        // }
        //
        // private void Load()
        // {
        //     DataPersistenceManager.instance.LoadGame();
        // }
        //
        // private IEnumerator SaveIEnumerable()
        // {
        //     DataPersistenceManager.instance.SaveGame();
        //     yield return new WaitUntil(() => DataPersistenceManager.instance.IsSaved);
        //     // while (!DataPersistenceManager.instance.IsSaved)
        //     // {
        //     //     Debug.Log("Wait");
        //     // }
        //     
        //     DataPersistenceManager.instance.IsSaved = false;
        //     
        // }
    }
}