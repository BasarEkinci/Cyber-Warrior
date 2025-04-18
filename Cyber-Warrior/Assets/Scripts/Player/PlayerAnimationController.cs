using Player;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private PlayerManager _playerManager;
    private Animator _animator;
    private void Awake()
    {
        _playerManager = GetComponent<PlayerManager>();
        _animator = GetComponent<Animator>();
    }
    private void Update()
    {
        _animator.SetFloat("Input", _playerManager.MoveVectorMagnitude);
    }
}
