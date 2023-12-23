using UnityEngine;

namespace Utils
{
    public class TimeManager : MonoBehaviour
    {

        public static TimeManager Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        public void PauseGame()
        {
           Time.timeScale = 0f;
        }

        public void UnpauseGame()
        {
            Time.timeScale = 1f;
        }

        public void ChangeGameTimeScale(float speed)
        {
            Time.timeScale = speed;
        }

    }
}