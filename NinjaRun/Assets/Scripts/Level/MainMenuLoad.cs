using System;
using DataPersistence;
using Services;
using UnityEngine;

namespace Level
{
    public class MainMenuLoad : MonoBehaviour
    {
        [SerializeField] private DataPersistenceManager manager;
        private Authentication authentication;

        private void Awake()
        {
            authentication = FindObjectOfType<Authentication>();
        }

        private void OnEnable()
        {
            authentication.OnSuccesAuthentication += Load;
        }
        private void OnDisable()
        {
            authentication.OnSuccesAuthentication -= Load;
        }

        private void Load()
        {
            if(manager!=null)
                manager.LoadGame();
        }


        // private void Start()
        // {
        //     if(manager !=null) 
        //         manager.LoadGame();
        // }
    }
}