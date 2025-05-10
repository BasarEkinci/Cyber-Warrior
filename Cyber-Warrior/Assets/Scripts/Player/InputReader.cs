using UnityEngine;
using Inputs;

namespace Player
{
    public class InputReader : IPlayerInput,IDisposable
    {
        private readonly InputActions _inputActions;

        public InputReader()
        {
            _inputActions = new InputActions();
            _inputActions.Player.Enable();
        }

        public Vector3 GetMovementVector()
        {
            Vector2 input = _inputActions.Player.Movement.ReadValue<Vector2>();
            Vector3 moveVector = new Vector3(input.x, 0, input.y);
            return new Vector3(moveVector.x, 0, moveVector.z);
        }

        public void Dispose()
        {
            _inputActions.Player.Disable();
        }
    }
}