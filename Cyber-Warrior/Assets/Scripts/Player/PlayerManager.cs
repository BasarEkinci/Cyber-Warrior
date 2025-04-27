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
        [SerializeField] private GameObject playerHead;
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
                _mousePosition = hit.point;
                crosshair.transform.position = new Vector3(_mousePosition.x,1.5f,_mousePosition.z);
                playerHead.transform.LookAt(crosshair.transform.position);
                Vector3 direction = (_mousePosition - transform.position).normalized;
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
            if (_moveVector.sqrMagnitude < 0.01f)
            {
                _rb.linearVelocity = new Vector3(0, _rb.linearVelocity.y, 0);
                return;
            }

            _rb.linearVelocity = new Vector3(_moveVector.x * playerStats.moveSpeed, _rb.linearVelocity.y, _moveVector.z * playerStats.moveSpeed);
        }

        private void UpdateAnimations()
        {
            if (_moveVector.sqrMagnitude < 0.01f)
            {
                _animator.SetFloat(MoveX, 0f);
                _animator.SetFloat(MoveY, 0f);
                return;
            }
            Vector3 moveDirection = new Vector3(_moveVector.x, 0, _moveVector.z).normalized;
            Vector3 aimDirection = (_mousePosition - transform.position).normalized;


            aimDirection.y = 0;
            aimDirection.Normalize();

            float dot = Vector3.Dot(moveDirection, aimDirection);
            Vector3 cross = Vector3.Cross(aimDirection, moveDirection);

            if (dot > 0.5f)
            {
                _animator.SetFloat(MoveY,1f);
                _animator.SetFloat(MoveX,0f);
                Debug.Log("Moving Forward");
            }
            else if (dot < -0.5f)
            {
                _animator.SetFloat(MoveY,-1f);
                _animator.SetFloat(MoveX,0f);
                Debug.Log("Moving Backward");
            }
            else
            {
                if (cross.y > 0)
                {
                    _animator.SetFloat(MoveY,0f);
                    _animator.SetFloat(MoveX,1f);
                }
                else if (cross.y < 0)
                {
                    _animator.SetFloat(MoveY,0f);
                    _animator.SetFloat(MoveX,-1f);
                }
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
