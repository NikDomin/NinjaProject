using System;
using Agent;
using DataPersistence;
using DataPersistence.Data;
using Level.Resettable;
using Services;
using TMPro;
using UnityEngine;

namespace Level
{
    public class PlayerEndlessRunScore : MonoBehaviour, IDataPersistence, IResettable
    {
        public static PlayerEndlessRunScore Instance;
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private Transform playerTransform;
        private int currentMaxPosition;
        private int score = 0;
        private bool isUpdateScore;
        private bool isBeginnerRunnerAlreadyCompleted;

        #region Mono

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
        }

        private void OnEnable()
        {
            playerTransform.GetComponent<PlayerHealth>().OnDead.AddListener(StopUpdateScore);  
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
            if (playerTransform.position.x > currentMaxPosition+1f)
            {
                score++;
                scoreText.text ="Score: " + score.ToString();
                currentMaxPosition = (int)playerTransform.position.x;
                
                if(!isBeginnerRunnerAlreadyCompleted && score >= 2000)
                    Achievement.Instance.BeginnerRunner();
            }
        }

        #endregion

        
        private void StopUpdateScore()
        {
            isUpdateScore = false;
            Social.ReportScore(score, "CgkI5fOH1boJEAIQAQ", (bool success) =>
            {
                
            });
            playerTransform.GetComponent<PlayerHealth>().OnDead.RemoveListener(StopUpdateScore);  

        }

        public void ContinueUpdateScore()
        {
            isUpdateScore = true;
            playerTransform.GetComponent<PlayerHealth>().OnDead.AddListener(StopUpdateScore);  
        }

        public void Reset()
        {
            currentMaxPosition = Convert.ToInt32(transform.position.x);
            score = 0;
            isUpdateScore = true;
        }

        #region SaveSystem

        public void LoadData(GameData data)
        {
            isBeginnerRunnerAlreadyCompleted = data.IsBeginnerRunnerAlreadyComplited;
        }

        public void SaveData(GameData data)
        {
            data.IsBeginnerRunnerAlreadyComplited = isBeginnerRunnerAlreadyCompleted;
        }

        #endregion
    }
}
