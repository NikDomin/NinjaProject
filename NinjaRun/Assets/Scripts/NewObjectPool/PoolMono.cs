using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace NewObjectPool
{
    public class PoolMono<T> where T : MonoBehaviour
    {
        public T prefab;
        public bool autoExpand;
        public Transform container;

        private List<T> pool;

        public PoolMono(T prefab, int count)
        {
            this.prefab = prefab;
            this.container = null;
            
            CreatePool(count);
        }

        public PoolMono(T prefab, int count, Transform container)
        {
            this.prefab = prefab;
            this.container = container;
            
            CreatePool(count);
        }

        public bool HasFreeElement( out T element)
        {
            foreach (var mono in pool)
            {
                if (!mono.gameObject.activeInHierarchy)
                {
                    element = mono;
                    return true;
                }
            }

            element = null;
            return false;
        }

        public T GetFreeElement()
        {
            if (this.HasFreeElement(out var element))
            {
                element.gameObject.SetActive(true);
                return element;
            }

            if (autoExpand)
                return CreateObject(true);

            throw new Exception($"There is no free elements in pool of type {typeof(T)}");
        }

        public void ReturnAllElement()
        {
            // for (int i = 0; i < pool.Count -1; i++)
            // {
            //     if (pool[i].gameObject.activeInHierarchy)
            //     {
            //         pool[i].gameObject.SetActive(false);
            //     }
            // }
            
            foreach (var mono in pool)
            {
                if (mono.gameObject.activeInHierarchy)
                    mono.gameObject.SetActive(false);
            }
        }

        public List<T> GetSpecifiedNumberItems(int number)
        {
            List<T> itemsList = new List<T>();
            for (int i = 0; i < number-1; i++)
            {
                itemsList.Add(GetFreeElement());
            }

            return itemsList;
        }
        

        private void CreatePool(int count)
        {
            pool = new List<T>();

            for (int i = 0; i < count; i++)
                CreateObject();
        }

        private T CreateObject(bool isActiveByDefault = false)
        {
            var createObject = Object.Instantiate(prefab, container);
            createObject.gameObject.SetActive(isActiveByDefault);
            pool.Add(createObject);

            return createObject;
        }
    }
}