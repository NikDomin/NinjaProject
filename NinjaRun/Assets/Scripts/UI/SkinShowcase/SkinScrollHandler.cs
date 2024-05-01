using System;
using DataPersistence;
using UnityEngine;

namespace UI.SkinShowcase
{
    public class SkinScrollHandler : MonoBehaviour
    {
        [SerializeField] private GameObject skinScroll;
        
        // private void Awake()
        // {
        //     skinScroll.SetActive(false);
        // }
        // private void OnEnable()
        // {
        //     DataPersistenceManager.instance.OnLoadEndSuccefully += ShowScroll;
        // }
        // private void OnDisable()
        // {
        //     DataPersistenceManager.instance.OnLoadEndSuccefully -= ShowScroll;
        // }
        //
        // public void HideScroll() => skinScroll.SetActive(false);
        // public void ShowScroll() => skinScroll.SetActive(true);

    }
}