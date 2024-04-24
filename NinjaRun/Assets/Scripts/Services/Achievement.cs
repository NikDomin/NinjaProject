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

        public void TestAchi()
        {
            Social.ReportProgress("CgkI5fOH1boJEAIQCA", 100.0f, (bool success) =>
            {
                
            });
        }
    }
}