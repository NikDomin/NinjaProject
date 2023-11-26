
using UnityEngine;

namespace Agent
{
    public class AgentBoxDetection : MonoBehaviour
    {
        [SerializeField] private bool debug;

        [field: SerializeField] public LayerMask DetectLayerMask { get; private set; }
        [field: SerializeField] public Vector2 DetectPoint { get; private set; }
        [field: SerializeField] public Vector2 Size { get; private set; }

        [SerializeField] private Color debugColor;
        
        private Vector2 offset;
        
        private Collider2D[] buffer = new Collider2D[6];
        public Collider2D[] Buffer => buffer;
        public Collider2D[] OverlapBox()
        {
            offset.Set(
                    transform.position.x + DetectPoint.x * transform.localScale.x, 
                    transform.position.y + DetectPoint.y
                );

            return Physics2D.OverlapBoxAll(offset, Size, 0f, DetectLayerMask);
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

        public int OverlapBoxNonAlloc(Collider2D[] _buffer)
        {
            offset.Set(
                    transform.position.x + DetectPoint.x * transform.localScale.x, 
                    transform.position.y + DetectPoint.y
                );
            return Physics2D.OverlapBoxNonAlloc(offset, Size, 0f,  buffer, DetectLayerMask);
        }
        
        private void OnDrawGizmos()
        {
            if (!debug)
                return;

            Gizmos.color = debugColor;
            Gizmos.DrawWireCube(((transform.position + (Vector3)DetectPoint)) * transform.localScale.x, Size);

        }
    }
}