using UnityEngine;

namespace Agent.Player
{
    public class PlayerStartPosition : MonoBehaviour
    {
        public Vector3 StartPosition { get; private set; }

        private void Awake()
        {
            StartPosition = new Vector3(
                        transform.position.x,
                        transform.position.y + 0.15f,
                        transform.position.z
                    );
        }
    }
}