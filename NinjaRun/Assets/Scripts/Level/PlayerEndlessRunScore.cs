using Agent;
using TMPro;
using UnityEngine;

namespace Level
{
    public class PlayerEndlessRunScore : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private Transform playerTransform;
        private int currentMaxPosition;
        private int score = 0;
        private bool isUpdateScore;

        private void OnEnable()
        {
            playerTransform.GetComponent<Health>().OnDead.AddListener(StopUpdateScore);  
        }

        private void Start()
        {
            currentMaxPosition = (int)transform.position.x;
            isUpdateScore = true;
        }

        private void FixedUpdate()
        {
            if(!isUpdateScore)
                return;
            if (playerTransform.position.x > currentMaxPosition)
            {
                score++;
                scoreText.text ="Score: " + score.ToString();
                currentMaxPosition = (int)playerTransform.position.x;
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
    }
}
