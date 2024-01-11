using UnityEngine;

namespace Movement
{
    public class PatrolMovement : MonoBehaviour
    {
        [SerializeField] private Transform firstPoint;
        [SerializeField] private Transform secondPoint;
        [SerializeField] private Transform surikenHandler;
        [SerializeField] private float patrolSpeed;

        private Transform currentPoint;
        
        private void Start()
        {
            currentPoint = firstPoint;
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