using Interfaces;
using ScriptableObjects.Events;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Inputs
{
    public class InputReader : IPlayerInput,IDisposable
    {
        public InputActions InputActions => _inputActions;
        private readonly InputActions _inputActions;
        private readonly HoldInputChannelSO _fireEventSo;
        public InputReader()
        {
            _inputActions = new InputActions();
            _inputActions.Player.Enable();
        }
        public InputReader(HoldInputChannelSO fireEventSo)
        {
            _inputActions = new InputActions();
            _inputActions.Player.Fire.started += OnFireStarted;
            _inputActions.Player.Fire.canceled += OnFireCanceled;
            _inputActions.Player.Enable();
            _fireEventSo = fireEventSo;
        }
        
        
        private void OnFireCanceled(InputAction.CallbackContext obj)
        {
            _fireEventSo.RaiseFireEnd();
        }
        private void OnFireStarted(InputAction.CallbackContext obj)
        {
            _fireEventSo.RaiseFireStart();
        }
        public Vector3 GetMovementVector()
        {
            Vector2 input = _inputActions.Player.Movement.ReadValue<Vector2>();
            Vector3 moveVector = new Vector3(input.x, 0, input.y);
            return new Vector3(moveVector.x, 0, moveVector.z);
        }
        public void UnsubscribeFromFireEvents()
        {
            _inputActions.Player.Fire.started -= OnFireStarted;
            _inputActions.Player.Fire.canceled -= OnFireCanceled;
        }
        public void Dispose()
        {
            _inputActions.Player.Disable();
        }
    }
}