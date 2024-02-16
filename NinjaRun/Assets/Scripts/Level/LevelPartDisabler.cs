using UnityEngine;

namespace Level
{
    public class LevelPartDisabler : MonoBehaviour
    {
        [SerializeField] private float disableDistance;
        [SerializeField] private float invokeCD;
        
        private LevelPool levelPool;
        private LevelPart levelPart;
        

        private void Awake()
        {
            levelPool = GetComponentInParent<LevelPool>();
            levelPart = GetComponent<LevelPart>();
        }

        private void Start()
        {
            InvokeRepeating(nameof(TryDisableLevelParts), 0, invokeCD);
        }

        private void TryDisableLevelParts()
        {
            if (transform.position.x + disableDistance < DeathWall.deathWall.transform.position.x)
            {
                levelPool.PutLevelPartIntoLevelSlot(gameObject, levelPart.ID);
            }
        }
    }
}