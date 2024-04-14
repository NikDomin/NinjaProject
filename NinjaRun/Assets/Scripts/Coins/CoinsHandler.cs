using System;
using DataPersistence;
using DataPersistence.Data;
using UnityEngine;

namespace Coins
{
    public class CoinsHandler : MonoBehaviour, IDataPersistence
    {
        public static CoinsHandler Instance;
        public event Action OnChangeCoinsCount;
        [field: SerializeField] public int CurrentCoins { get; private set; }
        
        public void AddCoins(int coins)
        {
            if (coins < 0)
            {
                Debug.LogError("you can't add a negative amount of coins");
                return;
            }
            
            CurrentCoins += coins;
            OnChangeCoinsCount?.Invoke();
        }

        public void RemoveCoins(int coins)
        {
            if (coins < 0)
            {
                Debug.LogError("you can't remove a negative amount of coins");
                return;
            }

            CurrentCoins -= coins;
            OnChangeCoinsCount?.Invoke();
        }
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }


        #region NewSaveSystem

        public void LoadData(GameData data)
        {
            CurrentCoins = data.CoinsCount;
            OnChangeCoinsCount?.Invoke();
        }

        public void SaveData(GameData data)
        {
            data.CoinsCount = CurrentCoins;
        }

        #endregion
       
    }
}