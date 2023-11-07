using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Movement
{
    public class PositionCheck
    {
        public static bool GroundCheck(Vector3 position, float radius, LayerMask layer)
        {
            return Physics2D.OverlapCircle(position, radius, layer);
        }

        public static bool ObstacleCheck(Vector3 position, Vector2 direction, float length, LayerMask layer)
        {
            return Physics2D.Raycast(position, direction, length, layer);
        }

    }
}