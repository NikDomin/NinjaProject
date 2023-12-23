using UnityEngine;

namespace Utils
{
    public class AgentUtils
    {
        public static void SpriteDirection(Transform transform, Vector2 direction)
        {
            Vector2 scale = transform.localScale;
            if (direction.x > 0)
                scale.x = 1;
            else if (direction.x < 0)
                scale.x = -1;
            transform.localScale = scale;
        }

        public static void SpriteDirection(Transform targetTransform, Transform gameObjectTransform)
        {
            Vector2 scale = targetTransform.localScale;
            if (gameObjectTransform.localScale.x > 0)
                scale.x = 1;
            else if (gameObjectTransform.localScale.x < 0)
                scale.x = -1;
            targetTransform.localScale = scale;

        }

        public static void NormalSpriteDirection(Transform transform)
        {
            if (transform.localScale.x == -1)
            {
                Vector2 scale = transform.localScale;
                scale.x = 1;
                transform.localScale = scale;
            }
        }

    }
}