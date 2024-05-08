using System;
using System.Collections.Generic;
using DataPersistence.Data.SerializableTypes;
using UnityEngine;
using UnityEngine.U2D.Animation;

namespace Agent.Player
{
    public class SpriteLibraryHandler : MonoBehaviour
    {
        public static SpriteLibraryHandler Instance;
        
        [SerializeField] private SpriteLibraryDictionary spriteLibraryDictionary;

        public Dictionary<int, SpriteLibraryAsset> SpriteLibraryDictionary { get; private set; }
        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SpriteLibraryDictionary = spriteLibraryDictionary.ToDictionary();
        }

        // private void OnValidate()
        // {
        //     Debug.Log("On validate SpriteLibraryDictionary");
        //     SpriteLibraryDictionary = spriteLibraryDictionary.ToDictionary();
        // }
    }

    [Serializable]
    public class SpriteLibraryDictionary
    {
        [SerializeField] private SpriteLibraryDictionaryItems[] spriteDictionaryItems;

        public Dictionary<int, SpriteLibraryAsset> ToDictionary()
        {
            Dictionary<int, SpriteLibraryAsset> newDictionary = new Dictionary<int, SpriteLibraryAsset>();
            foreach (var item in spriteDictionaryItems)
            {
                newDictionary.Add(item.id, item.SpriteLibraryAsset);
            }

            return newDictionary;
        }
    }

    [Serializable]
    public class SpriteLibraryDictionaryItems
    {
        public int id;
        public SpriteLibraryAsset SpriteLibraryAsset;
    }
}