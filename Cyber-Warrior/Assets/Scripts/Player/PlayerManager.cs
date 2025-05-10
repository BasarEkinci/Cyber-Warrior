using DG.Tweening;
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
        [SerializeField] private GameObject crosshair;
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
            if (crosshair == null) return;

            Vector3 directionToAim = crosshair.transform.position - transform.position;
            directionToAim.y = 0f;

            Vector3 currentForward = transform.forward;
            currentForward.y = 0f;

            float angle = Vector3.SignedAngle(currentForward.normalized, directionToAim.normalized, Vector3.up);

            if (angle > 45f)
            {
                RotatePlayer(90f);
            }
            else if (angle < -45f)
            {
                RotatePlayer(-90f);
            }
        }

        private void RotatePlayer(float yAngleDelta)
        {
            Vector3 newRotation = transform.rotation.eulerAngles + new Vector3(0f, yAngleDelta, 0f);
            transform.DORotate(newRotation, 0.1f).OnComplete(() =>
            {
                float currentYRotation = transform.rotation.eulerAngles.y;
                float normalizedY = Mathf.DeltaAngle(0f, currentYRotation);
                float snapY = Mathf.Round(normalizedY / 90f) * 90f;
                Vector3 newEuler = new Vector3(0f, snapY, 0f);
                transform.rotation = Quaternion.Euler(newEuler); 
            });
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
