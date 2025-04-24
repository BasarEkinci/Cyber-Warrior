using UnityEngine;

namespace Player
{
    public class PlayerAnimationController : MonoBehaviour
    {
        private PlayerManager _playerManager;
        private Animator _animator;
        private static readonly int Input1 = Animator.StringToHash("Input");

        private void Awake()
        {
            _playerManager = GetComponent<PlayerManager>();
            _animator = GetComponent<Animator>();
        }
        private void Update()
        {
            _animator.SetFloat(Input1, _playerManager.MoveVectorMagnitude);
        }
    }
}
