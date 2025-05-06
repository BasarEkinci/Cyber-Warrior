using DG.Tweening;
using ScriptableObjects;
using ScriptableObjects.Events;
using UnityEditor;
using UnityEngine;

namespace Player
{
    public class PlayerManager : MonoBehaviour
    {
        #region Public Fields

        public float MoveVectorMagnitude => _moveVector.magnitude;

        #endregion
        #region Serilized Fields
        [SerializeField] private PlayerStats playerStats;
        [SerializeField] private PlayerDeathEvent playerDeathEvent;
        [SerializeField] private GameObject crosshair;
        [SerializeField] private LayerMask groundLayerMask;
        #endregion
        #region Private Fields

        private InputActions _inputActions;
        private Rigidbody _rb;
        private Animator _animator;
        private Camera _cam;
        
        private Vector3 _moveVector;
        private Vector2 _inputVector;
        private Vector3 _mousePosition;
        
        private bool _canMove;
        private bool _isFacingForward = true;
        private float _lastDegree;
        private static readonly int MoveX = Animator.StringToHash("MoveX");
        private static readonly int MoveZ = Animator.StringToHash("MoveZ");

        #endregion
        #region Unity Methods

        private void Awake()
        {
            _inputActions = new InputActions();
            _rb = GetComponent<Rigidbody>();
            _animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            _inputActions.Player.Enable();
            playerDeathEvent.OnPlayerDeath += OnPlayerDeath;
            _canMove = true;
            _cam = Camera.main;
        }

        private void Update()
        {
            if (!_canMove)
            {
                return;
            }
            LookAtMoveDirection();
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void OnDisable()
        {
            _inputActions.Player.Disable();
        }

        #endregion
        #region My Methods
        private void OnPlayerDeath()
        {
            _canMove = false;
        }
        private void LookAtMoveDirection()
        {
            Vector3 moveInput = GetMovementData();
            Vector3 direction = (crosshair.transform.position - transform.position).normalized;
            float dot = Vector3.Dot(transform.forward, direction);
            if (dot < 0.6f && _isFacingForward)
            {
                transform.Rotate(0f,90f,0f);
            }
            else if (dot >= 0.6f && !_isFacingForward)
            {
                transform.Rotate(0f, 90f, 0f);
                _isFacingForward = true;
            }
            if (moveInput.magnitude < 0.01f)
            {
                _animator.SetFloat(MoveX, 0);
                _animator.SetFloat(MoveZ, 0);
                return;
            }
            Vector3 localInput = transform.InverseTransformDirection(moveInput);
            _animator.SetFloat(MoveX, localInput.x);
            _animator.SetFloat(MoveZ, localInput.z);
        }

        private void Move()
        {
            if (!_canMove) return;
            if (GetMovementData().sqrMagnitude < 0.01f)
            {
                _rb.linearVelocity = new Vector3(0, _rb.linearVelocity.y, 0);
                return;
            }
            Vector3 moveVector = GetMovementData();
            _rb.linearVelocity = new Vector3(moveVector.x * playerStats.moveSpeed, _rb.linearVelocity.y, moveVector.z * playerStats.moveSpeed);
        }

        private Vector3 GetMovementData()
        {
            _inputVector = _inputActions.Player.Movement.ReadValue<Vector2>();
            _moveVector = new Vector3(_inputVector.x, 0, _inputVector.y);
            return _moveVector;
        }
        #endregion
    }
}
