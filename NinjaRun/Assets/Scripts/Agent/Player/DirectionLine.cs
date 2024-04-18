using System.Collections;
using Input;
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
                if (Vector2.Distance(startSwipePosition, NewInputManager.Instance.PrimaryPosition()) < 20f)
                {
                    direction = (NewInputManager.Instance.PrimaryPosition() - startSwipePosition)/2;
                }
                else
                {
                    direction =  ( NewInputManager.Instance.PrimaryPosition() - startSwipePosition).normalized * 12f;
                }
                
                lineRenderer.SetPosition(1, transform.position + (Vector3)direction);
                yield return null;
            }
        }
    }
}