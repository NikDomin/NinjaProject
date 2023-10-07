using System.Collections;
using Assets.Scripts.Input;
using UnityEngine;

namespace Assets.Scripts.Movement
{
    public class NewSwipeDetection : MonoBehaviour
    {

        [SerializeField] private float minimumDistance = 0.2f;
        [SerializeField] private float maximumTime = 1f;

        //private NewInputManager playerInput;

        private Vector2 startPosition;
        private float startTime;
        private Vector2 endPosition;
        private float endTime;

        public void SwipeStart(Vector2 position, float time)
        {
            startPosition = position;
            startTime = time;
        }

        public void SwipeEnd(Vector2 position, float time)
        {
            endPosition = position;
            endTime = time;

            DetectSwipe();
            ResetAllValue();
        }

        private void DetectSwipe()
        {
            if (Vector3.Distance(startPosition, endPosition) >= minimumDistance &&
                (endTime - startTime) <= maximumTime)
            {
                Debug.DrawLine(startPosition, endPosition, Color.red, 5f);
            }

        }

        private void ResetAllValue()
        {
            startPosition = Vector2.zero;
            endPosition = Vector2.zero;
            startTime = 0;
            endTime = 0;

        }
    }
}