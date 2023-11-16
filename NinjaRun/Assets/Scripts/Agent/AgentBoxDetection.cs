using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Agent
{
    public class AgentBoxDetection : MonoBehaviour
    {
        [SerializeField] private LayerMask detectLayerMask;
        [SerializeField] private Vector2 detectPoint;
        [SerializeField] private Vector2 size;
        private Vector2 offset;

        public Collider2D[] OverlapBox()
        {
            offset.Set(
                    transform.position.x + detectPoint.x * transform.localScale.x, 
                    transform.position.y + detectPoint.y
                );

            return Physics2D.OverlapBoxAll(offset, size, 0f, detectLayerMask);
        }

        //private void FixedUpdate()
        //{
        //    var collires = OverlapBox();
        //    if (collires.Length == 0)
        //        return;

        //    foreach (var item in collires)
        //    {
        //        Debug.Log(item.name);
        //    }
        //}

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube((transform.position + (Vector3)detectPoint) * transform.localScale.x,  size);
        }
    }
}