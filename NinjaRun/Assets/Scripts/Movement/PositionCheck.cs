using UnityEngine;

namespace Movement
{
    public static class PositionCheck
    {
        public static bool GroundCheck(Vector3 position, float radius, LayerMask layer)
        {
            return Physics2D.OverlapCircle(position, radius, layer);
        }

        public static bool ObstacleCheck(Vector3 position, Vector2 direction, float length, LayerMask layer)
        {
            return Physics2D.Raycast(position, direction, length, layer);
        }

        public static bool PlatformCheck(Vector3 position, Vector2 direction, float length, LayerMask layer)
        {
            RaycastHit2D hit = Physics2D.Raycast(position, direction, length, layer);

            if (hit.transform.TryGetComponent(out PlatformEffector2D platformEffector2D))
            {
                return true;
            }
            else return false;
        }

    }
}