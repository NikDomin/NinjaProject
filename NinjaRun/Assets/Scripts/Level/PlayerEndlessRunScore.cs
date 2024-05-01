using System;
using Agent;
using DataPersistence;
using DataPersistence.Data;
using Services;
using TMPro;
using UnityEngine;

namespace Level
{
    public class PlayerEndlessRunScore : MonoBehaviour, IDataPersistence
    {
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private Transform playerTransform;
        private int currentMaxPosition;
        private int score = 0;
        private bool isUpdateScore;
        private bool isBeginnerRunnerAlreadyCompleted;
        
        private void OnEnable()
        {
            playerTransform.GetComponent<Health>().OnDead.AddListener(StopUpdateScore);  
        }

        private void Start()
        {
            currentMaxPosition = Convert.ToInt32(transform.position.x);
            isUpdateScore = true;
        }

        private void FixedUpdate()
        {
            if(!isUpdateScore)
                return;
            if (playerTransform.position.x > currentMaxPosition+0.8f)
            {
                score++;
                scoreText.text ="Score: " + score.ToString();
                currentMaxPosition = (int)playerTransform.position.x;
                
                if(!isBeginnerRunnerAlreadyCompleted && score >= 5000)
                    Achievement.Instance.BeginnerRunner();
            }
        }

        private void StopUpdateScore()
        {
            isUpdateScore = false;
            Social.ReportScore(score, "CgkI5fOH1boJEAIQAQ", (bool success) =>
            {
                
            });
            playerTransform.GetComponent<Health>().OnDead.RemoveListener(StopUpdateScore);  

        }

        public void LoadData(GameData data)
        {
            isBeginnerRunnerAlreadyCompleted = data.IsBeginnerRunnerAlreadyComplited;
        }

        public void SaveData(GameData data)
        {
            data.IsBeginnerRunnerAlreadyComplited = isBeginnerRunnerAlreadyCompleted;
        }
    }
}
