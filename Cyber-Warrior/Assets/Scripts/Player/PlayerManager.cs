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
            Ray ray = _cam.ScreenPointToRay(Input.mousePosition); 
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayerMask)) 
            { 
                _mousePosition = hit.point;
                crosshair.transform.position = new Vector3(_mousePosition.x,1f,_mousePosition.z);
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
