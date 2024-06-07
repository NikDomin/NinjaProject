using System.Collections;
using Input.Old_Input;
using Movement;
using UnityEngine;

namespace Input
{
    public class InputTrail : MonoBehaviour
    {
        [SerializeField] private GameObject trail;
        private NewSwipeDetection swipeDetection;
        
        private Coroutine trailCoroutine;
        private bool isTrailCoroutineRunning;
        
        

        private void Awake()
        {
            swipeDetection = GetComponent<NewSwipeDetection>();
        }

        private void OnEnable()
        {
            swipeDetection.OnSwipeStart += SwipeStart;
            swipeDetection.OnSwipeEnd += SwipeEnd;
        }



        private void OnDisable()
        {
            swipeDetection.OnSwipeStart -= SwipeStart;
            swipeDetection.OnSwipeEnd -= SwipeEnd;
            
            if(isTrailCoroutineRunning)
                StopCoroutine(trailCoroutine);
        }

        private void SwipeStart(Vector2 startSwipePosition)
        {
            //trail
            trail.SetActive(true);
            trail.transform.position = startSwipePosition;
            trailCoroutine = StartCoroutine(Trail());
        }
        private void SwipeEnd(Vector2 endSwipePosition)
        {
            trail.SetActive(false);
            StopCoroutine(trailCoroutine);
        }
        
        private IEnumerator Trail()
        {
            isTrailCoroutineRunning = true;
            while (true)
            {
                trail.transform.position = OldInputManager.Instance.GetCurrentPosition();
                yield return null;
                isTrailCoroutineRunning = false;
            }
        }
    }
}