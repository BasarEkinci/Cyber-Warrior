using ScriptableObjects;
using UnityEngine;

namespace Player
{
    public class PlayerManager : MonoBehaviour
    {
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
            _rb.linearVelocity += _moveVector * (playerStats.moveSpeed * Time.fixedDeltaTime);
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

        private void GetMovementData()
        {
            _inputVector = _inputActions.Player.Movement.ReadValue<Vector2>();
            _moveVector = new Vector3(_inputVector.x, 0, _inputVector.y);
        }
        #endregion
    }
}
