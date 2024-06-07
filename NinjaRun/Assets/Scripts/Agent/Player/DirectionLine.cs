using System.Collections;
using Input;
using Input.Old_Input;
using Movement;
using UnityEngine;

namespace Agent.Player
{
    public class DirectionLine : MonoBehaviour
    {
        [SerializeField] private LineRenderer lineRenderer;
        private NewSwipeDetection swipeDetection;
        private Coroutine lineRendererCoroutine;

        private Vector2 startSwipePosition;
        private Vector2 endSwipePosition;
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
        }

        private void SwipeStart(Vector2 startSwipePosition)
        {
            this.startSwipePosition = startSwipePosition; 
            
            lineRenderer.gameObject.SetActive(true);
            lineRenderer.positionCount = 2;
            lineRendererCoroutine = StartCoroutine(DrawDirectionLine());
        }

        private void SwipeEnd(Vector2 endSwipePosition)
        {
            this.endSwipePosition = endSwipePosition;
            
            lineRenderer.gameObject.SetActive(false);
            StopCoroutine(lineRendererCoroutine);
        }
        
        private IEnumerator DrawDirectionLine()
        {
            while (true)
            {
                Vector2 direction;
                lineRenderer.SetPosition(0, transform.position);
                if (Vector2.Distance(startSwipePosition, OldInputManager.Instance.GetCurrentPosition()) < 20f)
                {
                    direction = (OldInputManager.Instance.GetCurrentPosition() - startSwipePosition)/2;
                }
                else
                {
                    direction =  ( OldInputManager.Instance.GetCurrentPosition() - startSwipePosition).normalized * 12f;
                }
                
                lineRenderer.SetPosition(1, transform.position + (Vector3)direction);
                yield return null;
            }
        }
    }
}