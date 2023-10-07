using System.Collections;
using Assets.Scripts.Utils;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Input
{
    public class NewInputManager : MonoBehaviour
    {
        //new
        private PlayerInput playerInput;


        [SerializeField] private InputActionReference primaryTouch, touchPosition;

        public UnityEvent<Vector2, float> OnStartTouch;
        public UnityEvent<Vector2, float> OnEndTouch;

        private Camera mainCamera;

        private void Awake()
        {
            playerInput = GetComponent<PlayerInput>();

            mainCamera = Camera.main;
        }

        private void OnEnable()
        {
            touchPosition.action.performed += StartTouchPrimary;
            touchPosition.action.canceled += EndTouchPrimary;
        }

        private void OnDisable()
        {
            touchPosition.action.performed -= StartTouchPrimary;
            touchPosition.action.canceled -= EndTouchPrimary;

        }

        //Test
        private void Update()
        {
            touchPosition.action.ReadValue<Vector2>();
            if (touchPosition.action.WasReleasedThisFrame())
            {
                Debug.Log("Touch release ");
            }
        }

        private void EndTouchPrimary(InputAction.CallbackContext ctx)
        {
            OnStartTouch?.Invoke(Utils.ScreenUtils.ScreenToWorld(mainCamera, touchPosition.action.ReadValue<Vector2>()), (float)ctx.startTime);
            Debug.Log("End touch");

        }

        private void StartTouchPrimary(InputAction.CallbackContext ctx)
        {
            OnEndTouch?.Invoke(Utils.ScreenUtils.ScreenToWorld(mainCamera, touchPosition.action.ReadValue<Vector2>()), (float)ctx.startTime);
            Debug.Log("Start touch");

            //Test
            Vector3 position = mainCamera.ScreenToWorldPoint(touchPosition.action.ReadValue<Vector2>());
            position.z = 0;
            transform.position = position;
        }

        public Vector2 PrimaryPosition()
        {
            return ScreenUtils.ScreenToWorld(mainCamera, touchPosition.action.ReadValue<Vector2>());
        }
    }
}