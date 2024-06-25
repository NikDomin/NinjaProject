using Level.Resettable;
using UnityEngine;

namespace Movement
{
    public class PatrolMovement : MonoBehaviour, IResettable
    {
        [SerializeField] private Transform firstPoint;
        [SerializeField] private Transform secondPoint;
        [SerializeField] private Transform surikenHandler;
        [SerializeField] private float patrolSpeed;

        private Transform currentPoint;
        private Vector3 resetPosition;
        
        private void OnEnable()
        {
            resetPosition = surikenHandler.position;
        }

        private void Start()
        {
            currentPoint = firstPoint;
        }

        public void Reset()
        {
            currentPoint = firstPoint;
            surikenHandler.position = resetPosition;
            gameObject.SetActive(true);
            
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (Vector2.Distance(surikenHandler.position, firstPoint.position) < 1f)
                currentPoint = secondPoint;
            else if (Vector2.Distance(surikenHandler.position, secondPoint.position) < 1f)
                currentPoint = firstPoint;

            float step = patrolSpeed * Time.deltaTime;
            surikenHandler.position = Vector2.MoveTowards(surikenHandler.position, currentPoint.position, step);
        }
    }
}