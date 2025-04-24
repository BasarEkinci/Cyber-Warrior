using ScriptableObjects;
using ScriptableObjects.Events;
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
        [SerializeField] private LayerMask groundLayerMask;
        #endregion
        #region Private Fields

        private InputActions _inputActions;
        private Rigidbody _rb;
        private Animator _animator;
        private Camera _cam;
        
        private Vector3 _moveVector;
        private Vector2 _inputVector;
        
        private bool _canMove;
        
        private static readonly int MoveX = Animator.StringToHash("MoveX");
        private static readonly int MoveY = Animator.StringToHash("MoveY");

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
            GetMovementData();
            LookAtMoveDirection();
            UpdateAnimations();
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
                Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayerMask))
                {
                    Vector3 targetPoint = hit.point;
                    Vector3 direction = (targetPoint - transform.position).normalized;

                    direction.y = 0;

                    if (direction != Vector3.zero)
                    {
                        Quaternion lookRotation = Quaternion.LookRotation(direction);
                        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation,
                            playerStats.rotateSpeed * Time.deltaTime);
                    }
                }
        }

        private void Move()
        {
            if (!_canMove) return;

            Vector3 forward = transform.forward;
            Vector3 right = Vector3.right;

            Vector3 moveDirection = forward * _moveVector.z + right * _moveVector.x; 
            Vector3 moveVector = moveDirection.normalized * playerStats.moveSpeed;

            _rb.linearVelocity = new Vector3(moveVector.x, _rb.linearVelocity.y, moveVector.z);

            if (moveVector.magnitude < 0.1f)
            {
                _rb.linearVelocity = new Vector3(0, _rb.linearVelocity.y, 0);
            }
            
        }

        private void UpdateAnimations()
        {
            if (_moveVector.sqrMagnitude < 0.01f)
            {
                _animator.SetFloat(MoveX, 0f);
                _animator.SetFloat(MoveY, 0f);
                return;
            }
            Vector3 moveDir = Vector3.forward * _moveVector.z + Vector3.right * _moveVector.x;
            moveDir.Normalize();
            if (transform.right.x > 0)
            {
                _animator.SetFloat(MoveX, moveDir.x);
                _animator.SetFloat(MoveY, moveDir.z);   
            }
            else
            {
                _animator.SetFloat(MoveX, -moveDir.x);
                _animator.SetFloat(MoveY, moveDir.z);
            } 
        }

        private void GetMovementData()
        {
            _inputVector = _inputActions.Player.Movement.ReadValue<Vector2>();
            _moveVector = new Vector3(_inputVector.x, 0, _inputVector.y);
        }
        #endregion
    }
}
