using Inputs;
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
        private PlayerMovement _playerMovement;
        private PlayerRotator _playerRotator;
        private PlayerAnimator _playerAnimator;
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
            _inputReader = new PlayerInputReader();
            _playerMovement = new PlayerMovement(_rb, playerStats);
            _playerRotator = new PlayerRotator(transform, _crosshair);
            _playerAnimator = new PlayerAnimator(_animator);
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
            _playerAnimator.SetAnimations(_inputVector, transform);
            _playerRotator.LookAtAim();
        }

        private void FixedUpdate()
        {
            _playerMovement.Move(_inputVector, _canMove);
        }

        private void OnDisable()
        {
            if (_inputReader is PlayerInputReader disposableInput)
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
