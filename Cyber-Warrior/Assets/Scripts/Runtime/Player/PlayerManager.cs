using Runtime.Data.UnityObjects.Events;
using Runtime.Data.UnityObjects.ObjectData;
using Runtime.Data.ValueObjects;
using Runtime.Enums;
using Runtime.Inputs;
using Runtime.Movement;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Runtime.Player
{
    public class PlayerManager : MonoBehaviour
    {
        #region Serilized Fields
        [SerializeField] private PlayerStatsSO playerStatsSo;
        [SerializeField] private VoidEventSO voidEventSo;
        [SerializeField] private VoidEventSO levelSuccessEvent;
        [SerializeField] private GameStateEvent gameStateEvent;
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
        private GameState _gameState;
        #endregion
        
        #region Unity Methods
        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            _playerStatsData = playerStatsSo.playerStatsDataList[0];
            _mover = new Mover(_rb, _playerStatsData.moveSpeed);
            _crosshair = GameObject.FindWithTag("Crosshair");
            _rotator = new Rotator(transform, _crosshair);
            _movementAnimator = new MovementAnimator(_animator);
            voidEventSo.OnEventRaised += OnPlayerDeath;
            gameStateEvent.OnEventRaised += OnGameStateChanged;
            inputReader.OnMove += HandleMove;
            levelSuccessEvent.OnEventRaised += OnLevelSucceed;
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
            gameStateEvent.OnEventRaised += OnGameStateChanged;
            levelSuccessEvent.OnEventRaised -= OnLevelSucceed;
            inputReader.OnMove -= HandleMove;
        }

        #endregion
        #region My Methods
        private void OnLevelSucceed()
        {
            _canMove = false;
        }

        private void OnGameStateChanged(GameState arg0)
        {
            _gameState = arg0;
            _canMove = _gameState == GameState.Action || _gameState == GameState.Base;
        }
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
