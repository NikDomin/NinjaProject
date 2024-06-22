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
            manager = FindObjectOfType<DataPersistenceManager>();
            authentication.OnSuccesAuthentication += Load;
            authentication.OnFailedAuthentication += LoadWithFailedAuthenctication;
        }
        private void OnDisable()
        {
            authentication.OnSuccesAuthentication -= Load;
            authentication.OnFailedAuthentication -= LoadWithFailedAuthenctication;
        }


        private void Load()
        {
            if(manager!=null)
                manager.LoadGame();
        }

        private void LoadWithFailedAuthenctication()
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