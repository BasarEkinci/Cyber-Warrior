using ScriptableObjects;
using UnityEngine;

namespace Player
{
    public class PlayerMovement
    {
        private readonly Rigidbody _rigidbody;
        private readonly PlayerStats _stats;

        public PlayerMovement(Rigidbody rb, PlayerStats stats)
        {
            _rigidbody = rb;
            _stats = stats;
        }

        public void Move(Vector3 inputVector, bool canMove)
        {
            if (!canMove) return;

            if (inputVector.sqrMagnitude < 0.01f)
            {
                _rigidbody.linearVelocity = new Vector3(0, _rigidbody.linearVelocity.y, 0);
                return;
            }

            Vector3 moveVector = inputVector.normalized;
            moveVector.y = _rigidbody.linearVelocity.y;
            _rigidbody.linearVelocity = moveVector * _stats.moveSpeed;
        }
    }
}
