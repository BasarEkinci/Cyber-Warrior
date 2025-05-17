using System;
using Companion.Mode;
using Enums;
using Inputs;
using Movement;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Companion.Controller
{
    public class CmpMovementController : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float rotationSpeed;
        [SerializeField] private Vector3 followOffset;
        
        [SerializeField] private Material eyeMaterial;
        
        private InputReader _inputReader;
        private CmpModeManager _cmpModeManager;
        
        private GameObject _crosshair; 
        private GameObject _target;

        private Mover _mover;
        private Rotator _rotator;
        
        private void Awake()
        {
            _target = GameObject.FindWithTag("Player");
            _crosshair = GameObject.FindWithTag("Crosshair");
            _cmpModeManager = new CmpModeManager();
            _mover = new Mover(transform, speed);
            _rotator = new Rotator(transform, _target);
            _inputReader = new InputReader();
            eyeMaterial.color = Color.green;
        }

        private void OnEnable()
        {
            _inputReader.InputActions.Player.CompanionMode.performed += OnCompanionModeChanged;
        }
        private void OnDisable()
        {
            _inputReader.InputActions.Player.CompanionMode.performed -= OnCompanionModeChanged;
        }

        private void OnCompanionModeChanged(InputAction.CallbackContext obj)
        {
            _cmpModeManager.NextMode();
            _cmpModeManager.CurrentMode.SetProperties(eyeMaterial);
        }

        private void Update()
        {
            if (_target == null) return;
            _cmpModeManager.CurrentMode.SetAimMode(_rotator,_crosshair, rotationSpeed);
            _cmpModeManager.CurrentMode.ModeBehaviour();
        }
        private void FixedUpdate()
        {
            if (_target == null) return;
            _mover.FollowTargetWithTransformPosition(transform, _target.transform, speed * Time.fixedDeltaTime, followOffset);
        }
    }
}
