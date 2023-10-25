using System.Collections;
using Assets.Scripts.Movement;
using UnityEngine;

namespace Assets.Scripts.Agent.Player
{
    public class MovementComponent : MonoBehaviour
    {
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public float GroundCheckRadius { get; private set;}
        [field: SerializeField] public LayerMask GroundLayer { get; private set; }
        [field: SerializeField] public Vector3 GroundCheckPosition { get; private set;}
        [field:SerializeField] public Vector3 WallCheckPosition { get; private set; }
        [field: SerializeField] public float RayLenght { get; private set; }

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

            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.TransformPoint(WallCheckPosition), transform.TransformPoint(WallCheckPosition) + transform.right * RayLenght);
        }
    }
}