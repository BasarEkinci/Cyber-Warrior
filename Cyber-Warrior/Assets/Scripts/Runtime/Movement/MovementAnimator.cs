using UnityEngine;

namespace Movement
{
    public class MovementAnimator
    {
        private readonly Animator _animator;

        private static readonly int MoveX = Animator.StringToHash("MoveX");
        private static readonly int MoveZ = Animator.StringToHash("MoveZ");

        public MovementAnimator(Animator animator)
        {
            _animator = animator;
        }

        public void SetAnimations(Vector3 moveDirection, Transform pos)
        {
            Vector3 localInput = pos.InverseTransformDirection(moveDirection);

            float forwardDot = localInput.z;
            float rightDot = localInput.x;

            if (moveDirection.magnitude > 0.1f && pos != null)
            {
                _animator.SetFloat(MoveX, rightDot);
                _animator.SetFloat(MoveZ, forwardDot);
            }
            else
            {
                _animator.SetFloat(MoveX, 0);
                _animator.SetFloat(MoveZ, 0);
            }
        }
    }
}
