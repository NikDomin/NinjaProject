using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Level
{
    public class LevelPart : MonoBehaviour
    {
        [field:SerializeField]public int ID { get; private set; }
        [field:SerializeField] public Transform EndTransform { get; private set; }
    }
}