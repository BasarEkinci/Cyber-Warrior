using UnityEngine;

namespace Runtime.Movement
{
    public class Mover
    {
        private readonly Rigidbody _rigidbody;
        private readonly float _moveSpeed;
        public Mover(float moveSpeed)
        {
            _moveSpeed = moveSpeed;
        }
        public Mover(Rigidbody rb, float moveSpeed)
        {
            _rigidbody = rb;
            _moveSpeed = moveSpeed;
        }

        public void FollowTarget(Transform current, Transform target, Vector3 followOffset)
        {
            Vector3 desiredPosition = target.position + followOffset;
            current.position = Vector3.Lerp(current.position,desiredPosition,_moveSpeed);
        }

        public void MoveWithRb(Vector3 direction, bool canMove)
        {
            if (!canMove) return;

            if (direction.sqrMagnitude < 0.01f)
            {
                _rigidbody.linearVelocity = new Vector3(0, _rigidbody.linearVelocity.y, 0);
                return;
            }

            Vector3 moveVector = direction.normalized;
            moveVector.y = _rigidbody.linearVelocity.y;
            _rigidbody.linearVelocity = moveVector * _moveSpeed;
        }
    }
}
