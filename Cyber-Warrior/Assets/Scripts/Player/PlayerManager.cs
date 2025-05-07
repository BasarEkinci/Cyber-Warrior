using ScriptableObjects;
using ScriptableObjects.Events;
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
        private Vector3 _mousePosition;
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

            Vector3 forward = transform.forward;
            Vector3 right = transform.right;
            Vector3 moveDirection = forward * _inputVector.z + right * _inputVector.x; 
            Vector3 moveVector = moveDirection.normalized * playerStats.moveSpeed;
            _rb.linearVelocity = new Vector3(moveVector.x, _rb.linearVelocity.y, moveVector.z);
        }

        private void LookAtAim()
        {
            if (crosshair == null)
            {
                Debug.LogWarning("Crosshair reference is missing!");
                return;
            }
            Vector3 lookDirection = crosshair.transform.position - transform.position;
            lookDirection.y = 0;
            lookDirection.Normalize();
            Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, playerStats.rotateSpeed * Time.deltaTime);
        }
        
        private void SetAnimations()
        {
            if (_inputVector.magnitude > 0.1f && _cam != null)
            {
                
                Vector3 cameraForward = _cam.transform.forward;
                cameraForward.y = 0;
                cameraForward.Normalize();

                Vector3 cameraRight = _cam.transform.right;
                cameraRight.y = 0;
                cameraRight.Normalize();

                Vector3 moveDir = cameraForward * _inputVector.z + cameraRight * _inputVector.x;
                moveDir.Normalize();

                _animator.SetFloat(MoveX, moveDir.x);
                _animator.SetFloat(MoveZ, moveDir.z);
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
