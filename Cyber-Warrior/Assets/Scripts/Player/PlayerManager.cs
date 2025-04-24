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
        private Vector3 _moveVector;
        private Vector2 _inputVector;
        private bool _canMove;

        #endregion
        #region Unity Methods

        private void Awake()
        {
            _inputActions = new InputActions();
            _rb = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            _inputActions.Player.Enable();
            playerDeathEvent.OnPlayerDeath += OnPlayerDeath;
            _canMove = true;
        }

        private void OnPlayerDeath()
        {
            _canMove = false;
        }

        private void Update()
        {
            GetMovementData();
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
        private void LookAtMoveDirection()
        {
            if (_canMove)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
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

        private void GetMovementData()
        {
            _inputVector = _inputActions.Player.Movement.ReadValue<Vector2>();
            _moveVector = new Vector3(_inputVector.x, 0, _inputVector.y);
        }
        #endregion
    }
}
