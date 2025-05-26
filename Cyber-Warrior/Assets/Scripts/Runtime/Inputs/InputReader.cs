using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Inputs
{
    public class InputReader : MonoBehaviour, InputActions.IPlayerActions
    {
        public event Action<Vector2> OnMove;
        public event Action OnInteractPressed;
        public event Action OnSwitchMode;
        public event Action OnFireStarted;
        public event Action OnFireCanceled;
        public event Action OnInteractCanceled;
        
        private InputActions _inputActions;

        private void Awake()
        {
            _inputActions = new InputActions();
            _inputActions.Player.SetCallbacks(this);
        }

        private void OnEnable()
        {
            _inputActions.Player.Enable();
        }

        private void OnDisable()
        {
            _inputActions.Player.Disable();
        }

        public void OnMovement(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnMove?.Invoke(context.ReadValue<Vector2>());
            else if (context.canceled)
                OnMove?.Invoke(Vector2.zero);
        }

        public void OnFire(InputAction.CallbackContext context)
        {
            if (context.started)
                OnFireStarted?.Invoke();
            else if (context.canceled)
                OnFireCanceled?.Invoke();
        }

        public void OnCompanionMode(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnSwitchMode?.Invoke();
        }

        public void OnInteraction(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                OnInteractPressed?.Invoke();
            }
            else if (context.canceled)
            {
                OnInteractCanceled?.Invoke();
            }
        }
    }
}