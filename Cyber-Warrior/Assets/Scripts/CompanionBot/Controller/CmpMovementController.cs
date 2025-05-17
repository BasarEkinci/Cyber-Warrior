using CompanionBot.Mode;
using Inputs;
using Movement;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CompanionBot.Controller
{
    public class CmpMovementController : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float rotationSpeed;
        [SerializeField] private Vector3 followOffset;
        
        [SerializeField] private Material eyeMaterial;
        
        private InputReader _inputReader;
        private CmpBotModeManager _cmpBotModeManager;
        
        private GameObject _crosshair; 
        private GameObject _target;

        private Mover _mover;
        private Rotator _rotator;
        
        private void Awake()
        {
            _target = GameObject.FindWithTag("Player");
            _crosshair = GameObject.FindWithTag("Crosshair");
            _cmpBotModeManager = new CmpBotModeManager();
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
            _cmpBotModeManager.NextMode();
            _cmpBotModeManager.CurrentBotMode.SetProperties(eyeMaterial);
        }

        private void Update()
        {
            if (_target == null) return;
            _cmpBotModeManager.CurrentBotMode.SetAimMode(_rotator,_crosshair, rotationSpeed);
            _cmpBotModeManager.CurrentBotMode.ModeBehaviour();
        }
        private void FixedUpdate()
        {
            if (_target == null) return;
            _mover.FollowTargetWithTransformPosition(transform, _target.transform, speed * Time.fixedDeltaTime, followOffset);
        }
    }
}
