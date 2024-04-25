using GooglePlayGames;
using UnityEngine;

namespace Services
{
    public class Achievement : MonoBehaviour
    {
        public static Achievement Instance;
        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        public void NinjaApprentice()
        {
            Social.ReportProgress("CgkI5fOH1boJEAIQAg", 100.0f, (bool success) =>
            {
                
            });
        }

        public void BeatThemAll()
        {
            PlayGamesPlatform.Instance.IncrementAchievement("CgkI5fOH1boJEAIQAw", 1, (bool success) =>
            {
                
            });
        }

        public void Slayer()
        {
            PlayGamesPlatform.Instance.IncrementAchievement("CgkI5fOH1boJEAIQBA", 1, (bool success) =>
            {
                
            });
        }
        
        public void NinjaModel()
        {
            Social.ReportProgress("CgkI5fOH1boJEAIQBQ", 100.0f, (bool success) =>
            {
                
            });
        }

        public void BeginnerRunner()
        {
            Social.ReportProgress("CgkI5fOH1boJEAIQBg", 100.0f, (bool success) =>
            {
                
            });
        }

        public void Cheapskate()
        {
            Social.ReportProgress("CgkI5fOH1boJEAIQBw", 100.0f, (bool success) =>
            {
                
            });
        }
    }
}