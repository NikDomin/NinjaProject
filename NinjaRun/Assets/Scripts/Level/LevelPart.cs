using UnityEngine;

namespace Level
{
    public class LevelPart : MonoBehaviour
    {
        [field:SerializeField]public int ID { get; private set; }
        [field:SerializeField] public Transform EndTransform { get; private set; }
    }
}