using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Agent
{
    public class AgentBoxDetection : MonoBehaviour
    {
        [SerializeField] private bool debug;

        [field: SerializeField] public LayerMask detectLayerMask { get; private set; }
        [field: SerializeField] public Vector2 detectPoint { get; private set; }
        [field: SerializeField] public Vector2 size { get; private set; }

        [SerializeField] private Color debugColor;
        private Vector2 offset;

        public Collider2D[] OverlapBox()
        {
            offset.Set(
                    transform.position.x + detectPoint.x * transform.localScale.x, 
                    transform.position.y + detectPoint.y
                );

            return Physics2D.OverlapBoxAll(offset, size, 0f, detectLayerMask);
        }

        public Collider2D[] OverlapBox(float detectPointX, float detectPointY, float sizeX, float sizeY, LayerMask _detectLayerMask)
        {
            Vector2 _offset = Vector2.zero;
            _offset.Set(
                transform.position.x + detectPointX * transform.localScale.x,
                transform.position.y + detectPointY
                );

            return Physics2D.OverlapBoxAll(_offset, new Vector2(sizeX,sizeY), 0f, _detectLayerMask);
        }

        private void OnDrawGizmos()
        {
            if (!debug)
                return;

            Gizmos.color = debugColor;
            Gizmos.DrawWireCube(((transform.position + (Vector3)detectPoint)) * transform.localScale.x, size);

        }
    }
}