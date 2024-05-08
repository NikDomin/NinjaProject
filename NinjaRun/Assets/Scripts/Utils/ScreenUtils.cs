using UnityEngine;

namespace Utils
{
    public static class ScreenUtils
    {
        public static Vector3 ScreenToWorld(Camera camera, Vector3 position)
        {
            
            position.z = camera.nearClipPlane;
            return camera.ScreenToWorldPoint(position);
            
        }
        public static Vector3 WorldToScreen(Camera camera, Vector3 position)
        {
            return camera.WorldToScreenPoint(position);
        }
        
    }
}