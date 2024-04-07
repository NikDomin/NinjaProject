using Movement;
using NewObjectPool;
using UnityEngine;

namespace UI
{
    public class VisualSwipeHandler : MonoBehaviour
    {
        [SerializeField] private MonoBehaviour swipeMarks;
        [SerializeField] private NewSwipeDetection playerSwipeDetection;
        private PoolMono<MonoBehaviour> swipeMarksPool;

        private void Awake()
        {
            swipeMarksPool = new PoolMono<MonoBehaviour>(swipeMarks, 4, transform);
        }

        private void Start()
        {
            swipeMarksPool.ReturnAllElement();
            for (int i = 0; i < playerSwipeDetection.CurrentSwipeCount; i++)
            {
               var swipeMark = swipeMarksPool.GetFreeElement();
               swipeMark.gameObject.SetActive(true);
            }
        }

        private void FixedUpdate()
        {
            swipeMarksPool.ReturnAllElement();
            for (int i = 0; i < playerSwipeDetection.CurrentSwipeCount; i++)
            {
                var swipeMark = swipeMarksPool.GetFreeElement();
                swipeMark.gameObject.SetActive(true);
            }

        }
    }
}