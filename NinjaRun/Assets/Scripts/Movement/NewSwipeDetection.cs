using System.Collections;
using System.Threading.Tasks;
using Assets.Scripts.Input;
using UnityEngine;

namespace Assets.Scripts.Movement
{
    public class NewSwipeDetection : MonoBehaviour
    {

        [SerializeField] private float minimumDistance = 0.2f;
        [SerializeField] private float maximumTime = 1f;
        [SerializeField, Range(0,1f)] private float directionThreshold = 0.9f;

        [SerializeField] private GameObject trail;
        private NewInputManager playerInput;

        private Vector2 startPosition;
        private float startTime;
        private Vector2 endPosition;
        private float endTime;

        private bool alreadyStartTouch;
        private bool alreadyEndTouch;

        private Coroutine trailCoroutine;

        private void Awake()
        {
            playerInput = GetComponent<NewInputManager>();
        }

        #region SwipeHandler

        public void SwipeStartHandler(Vector2 position, float time)
        {
            if (alreadyStartTouch)
            {
                AwaitStartSwipe();
                return;
            }

            SwipeStart(position, time);
        }
        private async void AwaitStartSwipe()
        {
            await Task.Delay(10);
            alreadyStartTouch = false;
        }

        public void SwipeEndHandler(Vector2 position, float time)
        {
            if (alreadyEndTouch)
            {
                AwaitEndSwipe();
                return;
            }
            SwipeEnd(position, time);
        }

        private async void AwaitEndSwipe()
        {
            await Task.Delay(10);
            alreadyEndTouch = false;
        }

        #endregion

        
        private void SwipeStart(Vector2 position, float time)
        {
            Debug.Log("Swipe start");
            alreadyStartTouch = true;

            startPosition = position;
            startTime = time;
            
            //trail
            trail.SetActive(true);
            trail.transform.position = position;
            trailCoroutine = StartCoroutine(Trail());
        }

        private void SwipeEnd(Vector2 position, float time)
        {
            Debug.Log("Swipe end");
            alreadyEndTouch = true;

            endPosition = position;
            endTime = time;

            //trail
            trail.SetActive(false);
            StopCoroutine(trailCoroutine);

            DetectSwipe();
            //ResetAllValue();
        }

        private void DetectSwipe()
        {
            if (Vector3.Distance(startPosition, endPosition) >= minimumDistance &&
                (endTime - startTime) <= maximumTime)
            {
                Debug.DrawLine(startPosition, endPosition, Color.red, 5f);
            }

            //Detect direction with dot product
            Vector3 direction = endPosition - startPosition;
            Vector2 normalizedDirection = new Vector2(direction.x, direction.y).normalized;
            SwipeDirection(normalizedDirection);
        }
        

        //Detect direction with dot product
        private void SwipeDirection(Vector2 direction)
        {
            if(Vector2.Dot(Vector2.up, direction)> directionThreshold)
                Debug.Log("Swipe Up");
            else if (Vector2.Dot(Vector2.down, direction) > directionThreshold)
                Debug.Log("Swipe down");
            else if (Vector2.Dot(Vector2.right, direction)> directionThreshold)
                Debug.Log("SwipeRight");
            else if (Vector2.Dot(Vector2.left, direction) > directionThreshold)
                Debug.Log("Swipe left");

        }

        private void ResetAllValue()
        {
            startPosition = Vector2.zero;
            endPosition = Vector2.zero;
            startTime = 0;
            endTime = 0;

        }

        private IEnumerator Trail()
        {
            while (true)
            {
                trail.transform.position = playerInput.PrimaryPosition();
                yield return null;
            }
        }
    }
}