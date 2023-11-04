using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Level
{
    public class LevelPool : MonoBehaviour
    {
        [SerializeField] private LevelPartSlot[] levelParts;
        //private int[] levelPartsId;

        private void Awake()
        {
            foreach (var item in levelParts)
            {
                //for (int i = 0; i < levelParts.Length; i++)
                //{
                //    levelPartsId[i] = item.LevelPartPrefab.GetComponent<LevelPart>().ID;
                //}

                for (int i = 0; i < item.CountOnScreen; i++)
                {
                   InstantiateNewLevelPart(item);
                }
            }
        }

        private void InstantiateNewLevelPart(LevelPartSlot levelPartSlot)
        {
            LevelPart levelPartSpawned = Instantiate(levelPartSlot.LevelPartPrefab);
            levelPartSpawned.gameObject.SetActive(false);
            levelPartSlot.LevelPartSpawned.Add(levelPartSpawned.gameObject);
        }
        public GameObject RequestLevelPart(int ID)
        {
            LevelPartSlot slot = Array.Find(levelParts, element => element.LevelPartPrefab.ID == ID);
            if (slot == null)
            {
                Debug.LogError("Part " +ID + "not found" );
                return null;
            }

            if (slot.LevelPartSpawned.Count == 0)
            {
                InstantiateNewLevelPart(slot);
            }

            GameObject LevelPartToReturn = slot.LevelPartSpawned[0];
            LevelPartToReturn.SetActive(true);
            //here need to set position

            slot.LevelPartSpawned.Remove(LevelPartToReturn);
            return LevelPartToReturn;
        }

        public void PutLevelPartIntoLevelSlot(GameObject levelPartToPutBack, int ID)
        {
            LevelPartSlot slot = Array.Find(levelParts, element => element.LevelPartPrefab.ID == ID);
            if (slot == null)
            {
                Debug.LogError("Part " + ID + "not found");
                return;
            }

            levelPartToPutBack.SetActive(false);
            slot.LevelPartSpawned.Add(levelPartToPutBack);
        }

        //private Transform RequestEndPosition(int ID)
        //{
        //    LevelPartSlot slot = Array.Find(levelParts, element => element.LevelPartPrefab.ID == ID);
        //    if (slot == null)
        //    {
        //        Debug.LogError("Part " + ID + "not found");
        //        return null;
        //    }

        //    return slot.LevelPartPrefab.EndTransform;
        //}
    }

    [Serializable]
    public class LevelPartSlot
    {
        public LevelPart LevelPartPrefab;
        public List<GameObject> LevelPartSpawned;

        [field:SerializeField]public int CountOnScreen { get; private set; } = 2;
        //levelpartId;
    }
}