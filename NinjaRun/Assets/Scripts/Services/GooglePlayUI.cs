using System;
using UnityEngine;

namespace Services
{
    public class GooglePlayUI : MonoBehaviour
    {

      

        public void ShowLeaderBoard()
        {
            Social.ShowLeaderboardUI();
        }

        public void ShowAchievements()
        {
            Social.ShowAchievementsUI();
        }
    }
}