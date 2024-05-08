using System;
using DataPersistence;
using DataPersistence.Data;
using UnityEngine;
using UnityEngine.U2D.Animation;

namespace Agent.Player
{
    public class PlayerAnimationHandler : MonoBehaviour, IDataPersistence
    {
        public event Action OnAttack;

        [SerializeField] private SpriteLibrary spriteLibrary;
        private SpriteLibraryAsset spriteLibrarySpriteLibraryAsset;

        private void AttackActionTrigger() => OnAttack?.Invoke();

        //timed
        public void TestActionTrigger() => OnAttack?.Invoke();


        #region SaveSystem

        public void LoadData(GameData data)
        {
            var spriteDictionary = SpriteLibraryHandler.Instance.SpriteLibraryDictionary;
            int id = data.HeroSpriteLibraryID;
            
            spriteDictionary.TryGetValue(id, out spriteLibrarySpriteLibraryAsset);
            spriteLibrary.spriteLibraryAsset = spriteLibrarySpriteLibraryAsset;
        }

        public void SaveData(GameData data)
        {
            
        }

        #endregion
    }
}