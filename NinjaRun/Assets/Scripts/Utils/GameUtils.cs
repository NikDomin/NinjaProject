using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Utils
{
    public class GameUtils : MonoBehaviour
    {

        public static async void Timer(Action _timeEnd, int time)
        {
            await Task.Delay(time);
            _timeEnd?.Invoke();
        }
    }
}