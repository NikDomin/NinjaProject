using UnityEngine;

namespace Assets.Scripts.Movement
{
    public class PatrolMovement : MonoBehaviour
    {
        [SerializeField] private Transform firstPoint;
        [SerializeField] private Transform secondPoint;
        [SerializeField] private Transform SurikenHandler;
        [SerializeField] private float patrolSpeed;

        private Transform currentPoint;
        //private Rigidbody2D _rigidbody;

        private void Awake()
        {
            //_rigidbody = GetComponent<Rigidbody2D>();
        }
        private void Start()
        {
            currentPoint = firstPoint;
        }


        // Update is called once per frame
        void FixedUpdate()
        {
            if (Vector2.Distance(SurikenHandler.position, firstPoint.position) < 1f)
                currentPoint = secondPoint;
            else if (Vector2.Distance(SurikenHandler.position, secondPoint.position) < 1f)
                currentPoint = firstPoint;

            float step = patrolSpeed * Time.deltaTime;
            SurikenHandler.position = Vector2.MoveTowards(SurikenHandler.position, currentPoint.position, step);
        }
    }
}