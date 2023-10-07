using UnityEngine;

namespace Assets.Scripts.Movement
{
    public class SwipeDetection : MonoBehaviour
    {
        [SerializeField] private float minimumDistance = 0.2f;
        [SerializeField] private float maximumTime = 1f;

        private InputManager inputManager;

        private Vector2 startPosition;
        private float startTime;
        private Vector2 endPosition;
        private float endTime;

        private void Awake()
        {
            inputManager = GetComponent<InputManager>();
        }

        // Use this for initialization
        private void Start()
        {
            inputManager.OnStartTouch += SwipeStart;
            inputManager.OnEndTouch += SwipeEnd;
        }
        private void OnDestroy()
        {
            inputManager.OnStartTouch -= SwipeStart;
            inputManager.OnEndTouch -= SwipeEnd;
        }

        private void SwipeStart(Vector2 position, float time)
        {
            startPosition = position;
            startTime = time;
        }

        private void SwipeEnd(Vector2 position, float time)
        {
            endPosition = position;
            endTime = time;

            DetectSwipe();
        }

        private void DetectSwipe()
        {
            if (Vector3.Distance(startPosition, endPosition) >= minimumDistance &&
                 (endTime - startTime) <= maximumTime)
            {
                Debug.DrawLine(startPosition, endPosition, Color.red, 5f);
            }

        }

    }
}