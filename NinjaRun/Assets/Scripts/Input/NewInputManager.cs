using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Utils;

namespace Input
{
    public class NewInputManager : MonoBehaviour
    {
        //new
        public static NewInputManager Instance;
        public static PlayerInput PlayerInput;

        //Cancellation
        public CancellationTokenSource TokenSource { get; private set; }
        public CancellationToken Token { get; private set; }
        
        private bool isUseCancellationToken = false;
        

        [SerializeField] private InputActionReference primaryTouch, touchPosition, startPosition;

        //new
        private InputAction _primaryTouch, _touchPosition, _startPosition;

        public UnityEvent<Vector2, float> OnStartTouch;
        public UnityEvent<Vector2, float> OnEndTouch;

        private Camera mainCamera;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            //Token
            TokenSource = new CancellationTokenSource();
            Token = TokenSource.Token;
            

            PlayerInput = GetComponent<PlayerInput>();
            mainCamera = Camera.main;

            _primaryTouch = PlayerInput.actions["PrimaryContact"];
            _touchPosition = PlayerInput.actions["PrimaryPosition"];
            _startPosition = PlayerInput.actions["PrimaryStartPosition"];
        }

        private void OnEnable()
        {
            _primaryTouch.started += StartTouchPrimary;
            _primaryTouch.canceled += EndTouchPrimary;
        }

        private void OnDisable()
        {
            _primaryTouch.started -= StartTouchPrimary;
            _primaryTouch.canceled -= EndTouchPrimary;
        }

        private void OnDestroy()
        {
            TokenSource.Dispose();
            TokenSource = null;
        }

        public Vector2 PrimaryPosition()
        {
            return ScreenUtils.ScreenToWorld(mainCamera, touchPosition.action.ReadValue<Vector2>());
        }

        public void SwitchCancellationToken()
        {
            isUseCancellationToken = true;
        }

        private async void StartTouchPrimary(InputAction.CallbackContext ctx)
        {
            await Task.Delay(10);

            OnStartTouch?.Invoke(Utils.ScreenUtils.ScreenToWorld(mainCamera, 
                _startPosition.ReadValue<Vector2>()), (float)ctx.startTime);
            if (isUseCancellationToken)
            {
                TokenSource?.Cancel();
                isUseCancellationToken = false;
            }
        }
        
        private void EndTouchPrimary(InputAction.CallbackContext ctx)
        {
            OnEndTouch?.Invoke(Utils.ScreenUtils.ScreenToWorld(mainCamera,
                _touchPosition.ReadValue<Vector2>()), (float)ctx.startTime);
        }


        
        
    }
}