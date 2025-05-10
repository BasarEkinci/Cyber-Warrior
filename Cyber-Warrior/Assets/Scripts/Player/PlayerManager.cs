using DG.Tweening;
using ScriptableObjects;
using ScriptableObjects.Events;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Player
{
    public class PlayerManager : MonoBehaviour
    {
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
        private Vector3 _inputVector;
        
        private bool _canMove;
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

            _inputVector = GetMovementData();
            SetAnimations();
            LookAtAim();
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void OnDisable()
        {
            _inputActions.Player.Disable();
            playerDeathEvent.OnPlayerDeath -= OnPlayerDeath;
        }

        #endregion
        #region My Methods
        private void OnPlayerDeath()
        {
            _canMove = false;
        }
        
        private void Move()
        {
            if (!_canMove) return;
            if (_inputVector.sqrMagnitude < 0.01f)
            {
                _rb.linearVelocity = new Vector3(0, _rb.linearVelocity.y, 0);
                return;
            }

            Vector3 moveVector = _inputVector;
            moveVector.y = _rb.linearVelocity.y;
            moveVector.Normalize();
            _rb.linearVelocity = moveVector * playerStats.moveSpeed;
        }

        private void LookAtAim()
        {
            if (crosshair == null)
            {
                Debug.LogWarning("Crosshair reference is missing!");
                return;
            }
            Vector3 aimPosition = crosshair.transform.position;
            Vector3 playerPosition = transform.position;
            Vector3 targetVector = (aimPosition - playerPosition).normalized;
            Vector3 forward = transform.forward;
            forward.y = 0;
            targetVector.y = 0;
            forward.Normalize();
            targetVector.Normalize();
            float angle = Vector3.SignedAngle(forward, targetVector, Vector3.up);
            if (angle > 45)
            {
                transform.DORotate(transform.rotation.eulerAngles + new Vector3(0, 90f, 0), 0.1f);
            }
            else if (angle < -45f)
            {
                transform.DORotate(transform.rotation.eulerAngles + new Vector3(0, -90f, 0), 0.1f);
            }
        }
        
        private void SetAnimations()
        {
            Vector3 localInput = transform.InverseTransformDirection(_inputVector);
            
            float forwardDot = localInput.z;
            float rightDot = localInput.x;
            if (_inputVector.magnitude > 0.1f && _cam != null)
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

        private Vector3 GetMovementData()
        {
            Vector2 inputVector = _inputActions.Player.Movement.ReadValue<Vector2>();
            Vector3 moveVector = new Vector3(inputVector.x, 0, inputVector.y);
            return moveVector;
        }
        #endregion
    }
}
