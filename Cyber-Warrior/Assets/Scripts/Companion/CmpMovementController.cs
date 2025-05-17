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
        [SerializeField] private Material recoilMaterial;
        
        private InputReader _inputReader;
        private GameObject _crosshair; 
        private GameObject _target;
        private Mover _mover;
        private Rotator _rotator;
        private CmpModes _cmpMode;
        private int _cmpModeIndex;
        private void OnEnable()
        {
            _target = GameObject.FindWithTag("Player");
            _crosshair = GameObject.FindWithTag("Crosshair");
            _mover = new Mover(transform, speed);
            _rotator = new Rotator(transform, _target);
            _inputReader = new InputReader();
            _cmpModeIndex = 0;
            _cmpMode = CmpModes.CmpHealer;
            eyeMaterial.color = Color.green;
            _inputReader.InputActions.Player.CompanionMode.performed += OnCompanionModeChanged;
        }

        private void OnCompanionModeChanged(InputAction.CallbackContext obj)
        {
            _cmpModeIndex++;
            if (_cmpModeIndex > Enum.GetValues(typeof(CmpModes)).Length - 1)
            {
                _cmpModeIndex = 0;
            }
            _cmpMode = (CmpModes)_cmpModeIndex;
        }
        private void SetCompanionMode()
        {
            switch (_cmpMode)
            {
                case CmpModes.CmpAttacker:
                    eyeMaterial.color = Color.red;
                    AimAtCrosshair();
                    break;
                case CmpModes.CmpHealer:
                    eyeMaterial.color = Color.green;
                    _rotator.LookAtTarget();
                    break;
                case CmpModes.CmpBase:
                    Debug.Log("Base Mode");
                    eyeMaterial.color = Color.cyan;
                    break;
            }
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

        private void AimAtCrosshair()
        {
            if (_crosshair == null) return;
            Vector3 directionToAim = _crosshair.transform.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(directionToAim);
            Quaternion newRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
            transform.rotation = newRotation;
        }
    }
}
