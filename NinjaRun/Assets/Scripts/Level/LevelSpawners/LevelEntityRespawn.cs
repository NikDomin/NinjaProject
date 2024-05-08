using UnityEngine;

namespace Level.LevelSpawners
{
    public class LevelEntityRespawn : MonoBehaviour
    {
        [SerializeField] private GameObject[] entities;
        private void OnEnable()
        {
            foreach (var entity in entities)
            {
                entity.gameObject.SetActive(true);   
            }
        }
    }
}