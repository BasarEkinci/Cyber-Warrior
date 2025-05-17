using System;
using Enums;
using Inputs;
using Movement;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Companion
{
    public class CmpMovementController : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float rotationSpeed;
        [SerializeField] private Vector3 followOffset;
        
        [SerializeField] private Material eyeMaterial;
        
        private InputReader _inputReader;
        private GameObject _crosshair; 
        private GameObject _target;
        private Mover _mover;
        private Rotator _rotator;
        private CmpMode _cmpMode;
        private int _cmpModeIndex;
        
        private void OnEnable()
        {
            _target = GameObject.FindWithTag("Player");
            _crosshair = GameObject.FindWithTag("Crosshair");
            _mover = new Mover(transform, speed);
            _rotator = new Rotator(transform, _target);
            _inputReader = new InputReader();
            _cmpModeIndex = 0;
            _cmpMode = CmpMode.Healer;
            eyeMaterial.color = Color.green;
            _inputReader.InputActions.Player.CompanionMode.performed += OnCompanionModeChanged;
        }

        private void OnCompanionModeChanged(InputAction.CallbackContext obj)
        {
            _cmpModeIndex++;
            if (_cmpModeIndex > Enum.GetValues(typeof(CmpMode)).Length - 1)
            {
                _cmpModeIndex = 0;
            }
            _cmpMode = (CmpMode)_cmpModeIndex;
        }

        private void Update()
        {
            if (_target == null) return;
            SetCompanionMode();
        }
        private void FixedUpdate()
        {
            if (_target == null) return;
            _mover.FollowTargetWithTransformPosition(transform, _target.transform, speed * Time.fixedDeltaTime, followOffset);
        }
        private void SetCompanionMode()
        {
            switch (_cmpMode)
            {
                case CmpMode.Attacker:
                    eyeMaterial.color = Color.red;
                    _rotator.RotateToTarget(_crosshair,rotationSpeed * Time.deltaTime);
                    break;
                case CmpMode.Healer:
                    eyeMaterial.color = Color.green;
                    _rotator.SetLookDirection();
                    break;
                case CmpMode.Base:
                    eyeMaterial.color = Color.cyan;
                    _rotator.SetLookDirection();
                    break;
            }
        }
    }
}
