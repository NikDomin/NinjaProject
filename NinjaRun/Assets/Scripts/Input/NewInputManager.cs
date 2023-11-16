using System.Collections;
using System.Threading.Tasks;
using Assets.Scripts.Utils;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Input
{
    public class NewInputManager : MonoBehaviour
    {
        //new
        public static NewInputManager Instance;
        public static PlayerInput PlayerInput;
        //private PlayerControls playerControls;

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

            PlayerInput = GetComponent<PlayerInput>();
            mainCamera = Camera.main;


            //new
            _primaryTouch = PlayerInput.actions["PrimaryContact"];
            _touchPosition = PlayerInput.actions["PrimaryPosition"];
            _startPosition = PlayerInput.actions["PrimaryStartPosition"];
        }

        private void OnEnable()
        {


            //primaryTouch.action.started += StartTouchPrimary;
            //primaryTouch.action.canceled += EndTouchPrimary;

            //new
            _primaryTouch.started += StartTouchPrimary;
            _primaryTouch.canceled += EndTouchPrimary;
        }

        private void OnDisable()
        {
            //primaryTouch.action.started -= StartTouchPrimary;
            //primaryTouch.action.canceled -= EndTouchPrimary;

            //new
            _primaryTouch.started -= StartTouchPrimary;
            _primaryTouch.canceled -= EndTouchPrimary;


        }

        //Test
        private void Update()
        {
            //primaryTouch.action.ReadValue<Vector2>();
            //if (primaryTouch.action.WasReleasedThisFrame())
            //{
            //    Debug.Log("Touch release ");
            //}
        }

        private void EndTouchPrimary(InputAction.CallbackContext ctx)
        {
            OnEndTouch?.Invoke(Utils.ScreenUtils.ScreenToWorld(mainCamera, _touchPosition.ReadValue<Vector2>()), (float)ctx.startTime);
            //Debug.Log("End touch");

            

        }

        private async void StartTouchPrimary(InputAction.CallbackContext ctx)
        {
            await Task.Delay(10);

            OnStartTouch?.Invoke(Utils.ScreenUtils.ScreenToWorld(mainCamera, _startPosition.ReadValue<Vector2>()), (float)ctx.startTime);

           
        }

        public Vector2 PrimaryPosition()
        {
            return ScreenUtils.ScreenToWorld(mainCamera, touchPosition.action.ReadValue<Vector2>());
        }
    }
}