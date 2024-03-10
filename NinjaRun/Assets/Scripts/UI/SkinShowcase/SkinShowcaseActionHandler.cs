using System;
using UnityEngine;

namespace UI.SkinShowcase
{
    public class SkinShowcaseActionHandler : MonoBehaviour
    {
        public event Action<int> OnEquipSkin;
        
        [SerializeField] private SkinShowcase[] skinShowcases;


        public void InvokeOnEquipSkin(int id) => OnEquipSkin?.Invoke(id);
        
    }
}