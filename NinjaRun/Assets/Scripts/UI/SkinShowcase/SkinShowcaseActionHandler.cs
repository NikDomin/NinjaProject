using System;
using DataPersistence;
using DataPersistence.Data;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI.SkinShowcase
{
    public class SkinShowcaseActionHandler : MonoBehaviour, IDataPersistence
    {
        [FormerlySerializedAs("purchasedSkinsCount")] public int PurchasedSkinsCount;
        public event Action<int> OnEquipSkin;
     
        [SerializeField] private SkinShowcase[] skinShowcases;


        public void InvokeOnEquipSkin(int id) => OnEquipSkin?.Invoke(id);
   
        
        #region SaveSystem

        public void LoadData(GameData data)
        {
            PurchasedSkinsCount = data.PurchasedSkinsCount;

        }

        public void SaveData(GameData data)
        {
            if(PurchasedSkinsCount!<=0)
                data.PurchasedSkinsCount = PurchasedSkinsCount;
        }

        #endregion
    }
}