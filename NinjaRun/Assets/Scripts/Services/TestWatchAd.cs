using System.Collections.Generic;
using System.Linq;
using Agent.Player.PlayerStateMachine;
using Level;
using UnityEngine;

namespace Services
{
    public class TestWatchAd : MonoBehaviour
    {
        public static TestWatchAd Instance; 
        private bool alreadyWatchAd = false;
        public void Awake()
        {
            if (Instance == null)
                Instance = this;
        }

        public void WatchAd()
        {
            // if (alreadyWatchAd)
            // {
            //     // AwaitStartWatch();
            //     return;
            // }

            // alreadyWatchAd = true;
            var player = FindObjectOfType<PlayerState>(true).gameObject;
            var checkPoints = FindAllActiveCheckPoints();
            var firstCheckPoint = checkPoints
                .Where(checkpoint => checkpoint.transform.position.x < player.transform.position.x)
                .OrderByDescending(checkPoint => checkPoint.transform.position.x)
                .First();
            // CheckPoint checkPoint = checkPointsActivatedByPlayer.Max();
            firstCheckPoint.SpawnHero(player);
        }
        // private async void AwaitStartWatch()
        // {
        //     await Task.Delay(10);
        //     alreadyWatchAd = false;
        // }
        
        private List<CheckPoint> FindAllActiveCheckPoints()
        {
            var checkPoints = FindObjectsOfType<CheckPoint>();

            return new List<CheckPoint>(checkPoints);
        }
    }
}