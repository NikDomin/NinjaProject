using UnityEngine;

namespace Agent.Player
{
    public class PlayerStartPosition : MonoBehaviour
    {
        public Vector3 StartPosition { get; private set; }

        private void Awake()
        {
            StartPosition = transform.position;
        }
    }
}