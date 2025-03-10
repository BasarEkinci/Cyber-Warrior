using System;
using UnityEngine;

namespace Player
{
    public class PlayerManager : MonoBehaviour
    {
        #region Serilized Fields
        
        [SerializeField] private float moveSpeed;

        #endregion

        #region Private Fields
        
        private InputActions _inputActions;
        private Rigidbody _rb;
        
        private Vector3 _moveVector;
        private Vector2 _inputVector;

        #endregion

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
            _inputVector = _inputActions.Player.Movement.ReadValue<Vector2>();
            _moveVector = new Vector3(_inputVector.x, 0, _inputVector.y);
            transform.LookAt(transform.position + _moveVector);
            if (_moveVector.magnitude == 0)
            {
                _rb.linearVelocity = new Vector3(0, _rb.linearVelocity.y, 0);
            }
        }

        private void FixedUpdate()
        {
            _rb.linearVelocity += _moveVector * (moveSpeed * Time.fixedDeltaTime);
        }

        private void OnDisable()
        {
            _inputActions.Player.Disable();
        }
    }
}
