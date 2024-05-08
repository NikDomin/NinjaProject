using System;
using System.Threading.Tasks;
using Agent.Player.PlayerStateMachine;
using Input;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Movement
{
    public enum ForceType
    {
        withoutDrag,
        withDrag,
    }
    public enum JumpType
    {
        normalization,
        withoutNormalization
    }

    public class NewSwipeDetection : MonoBehaviour
    {
        public event Action<Vector2> OnSwipeStart;
        public event Action<Vector2> OnSwipeEnd;
        public event Action OnSwipe;

       

        [SerializeField] private float minimumDistance = 1f;
        [SerializeField] private float maximumTime = 1f;
        [SerializeField, Range(0,1f)] private float directionThreshold = 0.9f;
        [SerializeField] private int maxSwipeCount;
        [SerializeField] private int currentSwipeCount;

        [HideInInspector] public int CurrentSwipeCount
        {
            get
            {
                if (currentSwipeCount < 0)
                    return 0;
                return currentSwipeCount;
            }
            set
            {
                if (value < 0)
                    currentSwipeCount = 0;
                else currentSwipeCount = value;
            }
        }
        
        [field: SerializeField] public float impactsStrength { get; private set; }
        [field: SerializeField] public ForceType forceType { get; private set; }
        [field: SerializeField] public JumpType JumpType { get; private set; }

 
      

        public Vector3 directionSwipe { get; private set; }

        
        public Rigidbody2D _rigidbody2D { get; private set; }
        private PlayerState playerState;
        
        private Vector2 startPosition;
        private float startTime;
        private Vector2 endPosition;
        private float endTime;

        private bool alreadyStartTouch;
        private bool alreadyEndTouch;

      
       
        // FOR TEST
        

        #region Mono

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            playerState = GetComponent<PlayerState>();
            currentSwipeCount = maxSwipeCount;

            playerState.OnLanding += ResetSwipeCount;
        }
        
        private void OnDestroy()
        {
            playerState.OnLanding -= ResetSwipeCount;
            TimeManager.Instance.ChangeGameTimeScale(1);
        }

        #endregion

        #region SwipeHandler

        public void SwipeStartHandler(Vector2 position, float time)
        {
            Vector3 screenPoint = ScreenUtils.WorldToScreen(Camera.main, position);
            // Debug.Log("Start handler: " + screenPoint);
            if (screenPoint.x < Screen.width / 10f && screenPoint.y > Screen.height / 1.35f)
            {
                // ResetPositions();
                return;
            }
            if(screenPoint.x ==0 || screenPoint.y == 0)
                return;
      
            // Debug.Log("Start swipe position:" + position);
            if (alreadyStartTouch)
            {
                AwaitStartSwipe();
                return;
            }

            if (currentSwipeCount <= 0)
            {
                AwaitStartSwipe();
                return;
            }

            SwipeStart(position, time);
        }
        private async void AwaitStartSwipe()
        {
            await Task.Delay(80);
            alreadyStartTouch = false;
        }

        public void SwipeEndHandler(Vector2 position, float time)
        {
            // Debug.Log("Swipe end position:" + position);
            
            Vector3 screenPoint = ScreenUtils.WorldToScreen(Camera.main, position);
            // Debug.Log("End handler: " + screenPoint);
            if(screenPoint.x < Screen.width/10f && screenPoint.y > Screen.height/1.35f)
            {
                // ResetPositions();
                return;
            }
            
            if(screenPoint.x ==0 || screenPoint.y == 0)
                return;
            
            if (alreadyEndTouch)
            {
                AwaitEndSwipe();
                return;
            }

            if (currentSwipeCount <= 0)
            {
                AwaitEndSwipe();
                return;
            }
            
            SwipeEnd(position, time);
        }

        private async void AwaitEndSwipe()
        {
            await Task.Delay(80);
            alreadyEndTouch = false;
        }

        #endregion

        public void RefreshCurrentSwipeCount()
        {
            currentSwipeCount = maxSwipeCount;
        }
        
        private void SwipeStart(Vector2 position, float time)
        {
            alreadyStartTouch = true;
            startPosition = position;
            startTime = time;
            
            //ChangeTimeScale
            TimeManager.Instance.ChangeGameTimeScale(0.5f);

          

            var cursorPosition = NewInputManager.Instance.PrimaryPosition();
            OnSwipeStart?.Invoke(cursorPosition);
        }

        private void SwipeEnd(Vector2 position, float time)
        {
            alreadyEndTouch = true;
            endPosition = position;
            endTime = time;

            //ChangeTimeScale
            TimeManager.Instance.ChangeGameTimeScale(1);

            
            var cursorPosition = NewInputManager.Instance.PrimaryPosition();
            OnSwipeEnd?.Invoke(cursorPosition);
            
            DetectSwipe();
        }

        private void DetectSwipe()
        {
            // Debug.Log("#####################");
            // Debug.Log($"Vector swipe distance: {Vector3.Distance(startPosition,endPosition)}");
     
            if (Vector3.Distance(startPosition, endPosition) >= minimumDistance &&
                (endTime - startTime) <= maximumTime)
            {
                // Debug.Log("Jump swipe distance: "+ Vector3.Distance(startPosition, endPosition));
                --currentSwipeCount;
                
                Debug.DrawLine(startPosition, endPosition, Color.red, 5f);

                directionSwipe = endPosition - startPosition;
                OnSwipe?.Invoke();
                // ResetAllValue();
            }
        }
        

        //Detect direction with dot product
        private void SwipeDirection(Vector2 direction)
        {
            if(Vector2.Dot(Vector2.up, direction)> directionThreshold)
                Debug.Log("Swipe Up");
            else if (Vector2.Dot(Vector2.down, direction) > directionThreshold)
                Debug.Log("Swipe down");
            else if (Vector2.Dot(Vector2.right, direction)> directionThreshold)
                Debug.Log("SwipeRight");
            else if (Vector2.Dot(Vector2.left, direction) > directionThreshold)
                Debug.Log("Swipe left");

        }

        public void ResetAllValue()
        {
            startPosition = Vector2.zero;
            endPosition = Vector2.zero;
            startTime = 0;
            endTime = 0;
            alreadyStartTouch = false;
            alreadyEndTouch = false;
            ResetSwipeCount();
        }

        // private void ResetPositions()
        // {
        //     startPosition = Vector2.zero;
        //     endPosition = Vector2.zero;
        // }
        
        private void ResetSwipeCount()
        {
            currentSwipeCount = maxSwipeCount;
        }
    }
}