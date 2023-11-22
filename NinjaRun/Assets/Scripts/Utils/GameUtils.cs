using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Utils
{
    public class GameUtils : MonoBehaviour
    {

        public static async void Timer(Action _timeEnd, int time)
        {
            await Task.Delay(time);
            _timeEnd?.Invoke();
        }

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
    }
}