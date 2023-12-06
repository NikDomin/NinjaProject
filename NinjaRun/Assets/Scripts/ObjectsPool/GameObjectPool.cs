using Assets.Scripts.ObjectsPool;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ObjectsPool
{
    public class GameObjectPool : PoolBase<GameObject>
    {
        public GameObjectPool(GameObject prefab, int preloadCount) : 
            base(()=> Preload(prefab), GetAciton, ReturnAction, preloadCount)
        {

        }

        public static GameObject Preload(GameObject prefab) => Object.Instantiate(prefab);
        public static void GetAciton(GameObject _object) => _object.SetActive(true);
        public static void ReturnAction(GameObject _gameObject) => _gameObject.SetActive(false);
    }
}