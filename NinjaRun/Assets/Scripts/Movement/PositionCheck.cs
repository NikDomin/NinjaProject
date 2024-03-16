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

        public static bool ObstacleCheck(Vector3[] positions, Vector2 direction, float length, LayerMask layer)
        {
            bool isObstacle = false;
            foreach (var item in positions)
            {
                if (Physics2D.Raycast(item, direction, length, layer))
                {
                    return isObstacle = true;
                }
            }
            return isObstacle = false;
        }

    }
}