using UnityEngine;

namespace Movement
{
    public class Mover
    {
        private readonly Rigidbody _rigidbody;
        private readonly float _moveSpeed;
        public Mover(Rigidbody rb, float moveSpeed)
        {
            _rigidbody = rb;
            _moveSpeed = moveSpeed;
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
            _rigidbody.linearVelocity = moveVector * _moveSpeed;
        }
    }
}
