using ScriptableObjects;
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

        #endregion
        #region Private Fields

        private InputActions _inputActions;
        private Rigidbody _rb;
        private Vector3 _moveVector;
        private Vector2 _inputVector;

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
            if (_moveVector.magnitude > 0.01f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(_moveVector);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * playerStats.rotateSpeed);
            }
        }

        private void Move()
        {
            Vector3 targetVelocity = _moveVector * playerStats.moveSpeed;
            Vector3 velocity = _rb.linearVelocity;
            velocity.x = targetVelocity.x;
            velocity.y = _rb.linearVelocity.y;
            velocity.z = targetVelocity.z;
            _rb.linearVelocity = velocity;

            if (_moveVector.magnitude == 0)
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
