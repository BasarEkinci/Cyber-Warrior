using UnityEngine;

namespace Movement
{
    public class Mover
    {
        private readonly Rigidbody _rigidbody;
        private readonly float _moveSpeed;
        private Transform _transform;
        public Mover(Transform transform, float moveSpeed)
        {
            _transform = transform;
            _moveSpeed = moveSpeed;
        }
        public Mover(Rigidbody rb, float moveSpeed)
        {
            _rigidbody = rb;
            _moveSpeed = moveSpeed;
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
        public void FollowTargetWithRb(Transform target, float speed)
        {
            if (target == null) return;

            Vector3 direction = target.position - _rigidbody.transform.position;
            direction.y = 0f;
            direction.Normalize();
            _rigidbody.MovePosition(_rigidbody.transform.position + direction * speed * Time.deltaTime);
        }

        public void FollowTargetWithTransformPosition(Transform current,Transform target, float speed,Vector3 followOffset)
        {
            if (target == null) return;
            Vector3 desiredPosition = target.position + target.TransformDirection(followOffset);
            current.position = Vector3.Lerp(current.position, desiredPosition, speed);
        }
    }
}
