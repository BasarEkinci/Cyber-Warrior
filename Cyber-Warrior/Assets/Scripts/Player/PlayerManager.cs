using Inputs;
using Movement;
using ScriptableObjects;
using ScriptableObjects.Events;
using UnityEngine;

namespace Player
{
    public class PlayerManager : MonoBehaviour
    {
        #region Serilized Fields
        [SerializeField] private PlayerStats playerStats;
        [SerializeField] private PlayerDeathEvent playerDeathEvent;
        private GameObject _crosshair;
        #endregion
        #region Private Fields
        private Mover _mover;
        private Rotator _rotator;
        private MovementAnimator _movementAnimator;
        private IPlayerInput _inputReader;
        private Rigidbody _rb;
        private Animator _animator;
        private Camera _cam;
        
        private Vector3 _inputVector;
        
        private bool _canMove;

        #endregion
        #region Unity Methods

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            _crosshair = GameObject.FindWithTag("Crosshair");
            _inputReader = new InputReader();
            _mover = new Mover(_rb, playerStats.moveSpeed);
            _rotator = new Rotator(transform, _crosshair);
            _movementAnimator = new MovementAnimator(_animator);
            playerDeathEvent.OnPlayerDeath += OnPlayerDeath;
            _cam = Camera.main;
            _canMove = true;
            if (_cam == null)
            {
                Debug.LogError("Main Camera not found in scene!");
            }
        }

        private void Update()
        {
            if (!_canMove)
            {
                return;
            }
            _inputVector = _inputReader.GetMovementVector();
            _movementAnimator.SetAnimations(_inputVector, transform);
            _rotator.LookAtTarget();
        }

        private void FixedUpdate()
        {
            _mover.Move(_inputVector, _canMove);
        }

        private void OnDisable()
        {
            if (_inputReader is InputReader disposableInput)
            {
                disposableInput.Dispose();
            }
            playerDeathEvent.OnPlayerDeath -= OnPlayerDeath;
        }

        #endregion
        #region My Methods
        private void OnPlayerDeath()
        {
            _canMove = false;
        }
        #endregion
    }
}
