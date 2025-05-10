using UnityEngine;

namespace Player
{
    public class PlayerAnimator
    {
        private readonly Animator _animator;

        private static readonly int MoveX = Animator.StringToHash("MoveX");
        private static readonly int MoveZ = Animator.StringToHash("MoveZ");

        public PlayerAnimator(Animator animator)
        {
            _animator = animator;
        }

        public void SetAnimations(Vector3 inputVector, Transform pos)
        {
            Vector3 localInput = pos.InverseTransformDirection(inputVector);

            float forwardDot = localInput.z;
            float rightDot = localInput.x;

            if (inputVector.magnitude > 0.1f && pos != null)
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
