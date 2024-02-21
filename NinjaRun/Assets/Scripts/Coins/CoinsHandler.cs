using System;
using DataPersistence;
using DataPersistence.Data;
using SaveSystem;
using UnityEngine;

namespace Coins
{
    public class CoinsHandler : MonoBehaviour, IDataPersistence
    {
        public static CoinsHandler coinsHandler;
        [field: SerializeField] public int CurrentCoins { get; private set; }
        
        // #region SaveTest
        //
        // [SerializeField] private bool encryptionEnabled;
        //
        // private IDataService dataService = new JsonDataService();
        // private long SaveTime;
        //
        // [ContextMenu("Serialize Json")]
        // public void SerializeJson()
        // {
        //     long startTime = DateTime.Now.Ticks;
        //     
        //     if (dataService.SaveData("/Coins-Handler.json", CurrentCoins, encryptionEnabled))
        //     {
        //         SaveTime = DateTime.Now.Ticks - startTime;
        //         Debug.Log($"Save time: {(SaveTime/1000f):N4}ms");
        //     }
        //     else
        //     {
        //         Debug.LogError("Could not save file");
        //     }
        // }
        //
        // #endregion
        
        
        
        public void AddCoins(int coins)
        {
            if (coins < 0)
            {
                Debug.LogError("you can't add a negative amount of coins");
                return;
            }
            
            CurrentCoins += coins;
        }

        public void RemoveCoins(int coins)
        {
            if (coins < 0)
            {
                Debug.LogError("you can't remove a negative amount of coins");
                return;
            }

            CurrentCoins -= coins;
        }
        
        private void Awake()
        {
            coinsHandler = this;
        }


        #region NewSaveSystem

        public void LoadData(GameData data)
        {
            CurrentCoins = data.CoinsCount;
        }

        public void SaveData(GameData data)
        {
            data.CoinsCount = CurrentCoins;
        }

        #endregion
       
    }
}