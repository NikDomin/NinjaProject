using System;
using System.Threading.Tasks;
using Traps;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Utils
{
    public class GameUtils
    {
        // public static event Action OnTimerEnd;
        
        // public static async void Timer( Action action, int time)
        // {
        //     await Task.Delay(time); 
        //     action?.Invoke();
        // }

        public static int SceneNumber(Scene currentScene)
        {
            string sceneName = currentScene.name;

            if (sceneName.Contains("Level "))
            {
                string sceneNumberString = sceneName.Substring("Level ".Length);

                if (int.TryParse(sceneNumberString, out int sceneNumber))
                {
                    return sceneNumber;
                    //Debug.Log("Номер текущей сцены: " + sceneNumber);
                }
                else
                {
                    Debug.LogError("Failed to convert the scene index to a number");
                    return -1;
                }

            }
            else
            {
                Debug.LogError("The scene name does not contain a substring 'Level '.");
                return -1;
            }
            
        }

        public static Quaternion GetRotation(Vector2 direction)
        {
            // Calculate the rotation angle in degrees
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Create a target rotation based on the angle
            return Quaternion.Euler(0f, 0f, angle);
        }

        public static Vector3 GetDirection(Direction direction)
        {
            if (direction == Direction.down)
                return Vector3.down;
            else if (direction == Direction.right)
                return Vector3.right;
            else if (direction == Direction.up)
                return Vector3.up;
            else if (direction == Direction.left)
                return Vector3.left;
            else if (direction == Direction.upRight)
                return new Vector3(1, 1, 0);
            else if (direction == Direction.upLeft)
                return new Vector3(-1, 1, 0);
            else if (direction == Direction.downRight)
                return new Vector3(1, -1, 0);
            else if (direction == Direction.downLeft)
                return new Vector3(-1, -1, 0);
            else return Vector3.zero;
        }
    }
}