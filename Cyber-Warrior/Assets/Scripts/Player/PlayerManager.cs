using Data.UnityObjects;
using Inputs;
using Movement;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Player
{
    public class PlayerManager : MonoBehaviour
    {
        #region Serilized Fields
        [SerializeField] private PlayerStatsSO playerStatsSo;
        [SerializeField] private VoidEventSO voidEventSo;
        [SerializeField] private RigBuilder rigBuilder;
        [SerializeField] private InputReader inputReader;
        #endregion

        #region Private Fields
        private Mover _mover;
        private Rotator _rotator;
        private MovementAnimator _movementAnimator;
        
        private PlayerStatsData _playerStatsData;
        private GameObject _crosshair;
        private Rigidbody _rb;
        private Animator _animator;
        
        private Vector2 _inputVector;
        private Vector3 _moveVector;
        private bool _canMove;
        private int _currentLevel;
        #endregion
        
        #region Unity Methods
        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            _playerStatsData = playerStatsSo.playerStatsDataList[_currentLevel];
            _mover = new Mover(_rb, _playerStatsData.moveSpeed);
            _crosshair = GameObject.FindWithTag("Crosshair");
            _rotator = new Rotator(transform, _crosshair);
            _movementAnimator = new MovementAnimator(_animator);
            voidEventSo.OnEventRaised += OnPlayerDeath;
            inputReader.OnMove += HandleMove;
            _canMove = true;
        }

        private void Update()
        {
            if (!_canMove)
            {
                return;
            }
            _movementAnimator.SetAnimations(_moveVector, transform);
            _rotator.SetLookDirection();
        }

        private void FixedUpdate()
        {
            _mover.MoveWithRb(_moveVector, _canMove);
        }
        private void OnDisable()
        {
            voidEventSo.OnEventRaised -= OnPlayerDeath;
            inputReader.OnMove -= HandleMove;
        }

        #endregion
        #region My Methods
        private void OnPlayerDeath()
        {
            rigBuilder.enabled = false;
            _rb.linearVelocity = Vector3.zero;
            _canMove = false;
        }

        private void HandleMove(Vector2 input)
        {
            _inputVector = input;
            _moveVector = new Vector3(_inputVector.x, 0, _inputVector.y);
        }
        #endregion
    }
}
