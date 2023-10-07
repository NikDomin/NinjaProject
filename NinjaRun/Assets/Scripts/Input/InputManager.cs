using Assets.Scripts.Utils;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts
{
    public class InputManager : MonoBehaviour
    {
        #region events

        public delegate void StartTouch(Vector2 position, float time);
        public event StartTouch OnStartTouch;


        public delegate void EndTouch(Vector2 position, float time);
        public event EndTouch OnEndTouch;

        #endregion


        private PlayerControls playerControls;
        private Camera mainCamera;

        private void Awake()
        {
            playerControls = new PlayerControls();
            mainCamera = Camera.main;
        }


        private void OnEnable()
        {
            playerControls.Enable();
        }
        private void OnDisable()
        {
            playerControls.Disable();
        }

        // Start is called before the first frame update
        private void Start()
        {
            playerControls.Touch.PrimaryContact.started += ctx => StartTouchPrimary(ctx);
            playerControls.Touch.PrimaryContact.canceled += ctx => EndTouchPrimary(ctx);
        }

        private void StartTouchPrimary(InputAction.CallbackContext ctx)
        {
            if (OnStartTouch != null) //if something subscribe to event
                OnStartTouch(Utils.ScreenUtils.ScreenToWorld(mainCamera, playerControls.Touch.PrimaryPosition.ReadValue<Vector2>()), (float)ctx.startTime);
        }

        private void EndTouchPrimary(InputAction.CallbackContext ctx)
        {
            if (OnEndTouch!= null) //if something subscribe to event
                OnEndTouch(Utils.ScreenUtils.ScreenToWorld(mainCamera, playerControls.Touch.PrimaryPosition.ReadValue<Vector2>()), (float)ctx.time);
        }

        public Vector2 PrimaryPosition()
        {
            return ScreenUtils.ScreenToWorld(mainCamera, playerControls.Touch.PrimaryPosition.ReadValue<Vector2>());
        }

    }
}
