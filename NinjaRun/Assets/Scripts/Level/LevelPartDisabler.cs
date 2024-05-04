using System.Collections.Generic;
using System.Linq;
using Agent.Player.PlayerStateMachine;
using UnityEngine;

namespace Level
{
    public class LevelPartDisabler : MonoBehaviour
    {
        [SerializeField] private float disableDistance;
        [SerializeField] private float invokeCD;
        [SerializeField] private bool isHasCheckPoint;
        
        private NewLevelPool levelPool;
        private LevelPart levelPart;
        private Transform player;

        private void Awake()
        {
            levelPool = GetComponentInParent<NewLevelPool>();
            levelPart = GetComponent<LevelPart>();
            if(isHasCheckPoint) 
                player = FindObjectOfType<PlayerState>().transform;
        }

        private void Start()
        {
            InvokeRepeating(nameof(TryDisableLevelParts), 0, invokeCD);
        }

        private void TryDisableLevelParts()
        {
            if (!DeathWall.deathWall.CanDisableLevelParts)
                return;
            if (transform.position.x + disableDistance < DeathWall.deathWall.transform.position.x)
            {
                if (isHasCheckPoint)
                {
                    var LeftCheckPoints = FindAllActiveCheckPoints().Where(
                        checkPoint => checkPoint.transform.position.x < player.transform.position.x);
                    
                    if(LeftCheckPoints.Count() > 3) 
                        gameObject.SetActive(false);
                }
                else
                {
                    gameObject.SetActive(false);
                }
            }
        }
        
        private List<CheckPoint> FindAllActiveCheckPoints()
        {
            var checkPoints = FindObjectsOfType<CheckPoint>();

            return new List<CheckPoint>(checkPoints);
        }
    }
}