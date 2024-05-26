using Movement;
using UnityEngine;

namespace Agent.Player
{
    public class MovementComponent : MonoBehaviour
    {
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public float GroundCheckRadius { get; private set;}
        [field: SerializeField] public LayerMask GroundLayer { get; private set; }
        [field: SerializeField] public Vector3 GroundCheckPosition { get; private set;}
        [field: SerializeField] public Vector3 CeilingCheckPosition { get; private set; }
        [field: SerializeField] public float WallRayLength { get; private set; }
        [field: SerializeField] public float CeilRayLength { get; private set; }
        
        [field: SerializeField] public AgentBoxDetection WallDetection { get; private set; }

        private NewSwipeDetection swipeDetection;
        public Rigidbody2D _rigidbody2D;

        private bool isGrounded = false;
        private bool isOnWall = false;

        private void Awake()
        {
            swipeDetection = GetComponent<NewSwipeDetection>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.TransformPoint(GroundCheckPosition), GroundCheckRadius);
            
            //Ceiling Line
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.TransformPoint(CeilingCheckPosition), transform.TransformPoint(CeilingCheckPosition) + transform.up * CeilRayLength);
        }
    }
}