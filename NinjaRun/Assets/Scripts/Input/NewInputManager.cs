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
        //private PlayerInput playerInput;


        [SerializeField] private InputActionReference primaryTouch, touchPosition, startPosition;

        public UnityEvent<Vector2, float> OnStartTouch;
        public UnityEvent<Vector2, float> OnEndTouch;

        private Camera mainCamera;

        private void Awake()
        {

            mainCamera = Camera.main;
        }

        private void OnEnable()
        {
            primaryTouch.action.started += StartTouchPrimary;
            primaryTouch.action.canceled += EndTouchPrimary;
        }

        private void OnDisable()
        {
            primaryTouch.action.started -= StartTouchPrimary;
            primaryTouch.action.canceled -= EndTouchPrimary;

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
            OnStartTouch?.Invoke(Utils.ScreenUtils.ScreenToWorld(mainCamera, touchPosition.action.ReadValue<Vector2>()), (float)ctx.startTime);
            Debug.Log("End touch");

        }

        private void StartTouchPrimary(InputAction.CallbackContext ctx)
        {
            
            OnEndTouch?.Invoke(Utils.ScreenUtils.ScreenToWorld(mainCamera, startPosition.action.ReadValue<Vector2>()), (float)ctx.startTime);
            Debug.Log("Start touch");

            //Test
            //Vector3 position = mainCamera.ScreenToWorldPoint(startPosition.action.ReadValue<Vector2>());
            //position.z = 0;
            //transform.position = position;
        }

        public Vector2 PrimaryPosition()
        {
            return ScreenUtils.ScreenToWorld(mainCamera, touchPosition.action.ReadValue<Vector2>());
        }
    }
}