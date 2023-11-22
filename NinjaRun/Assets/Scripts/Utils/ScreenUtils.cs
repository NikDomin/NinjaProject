using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Utils
{
    public class ScreenUtils
    {
        public static Vector3 ScreenToWorld(Camera camera, Vector3 position)
        {
            position.z = camera.nearClipPlane;
            return camera.ScreenToWorldPoint(position);
        }

        
    }
}