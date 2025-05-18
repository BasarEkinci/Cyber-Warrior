using CompanionBot.Mode;
using Inputs;
using Movement;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CompanionBot.Controller
{
    public class CmpManager : MonoBehaviour
    {
        [SerializeField] private float attackCooldown;
        [SerializeField] private float damage;
        [SerializeField] private ParticleSystem attackEffect;
        
        [SerializeField] private Material eyeMaterial;
        [SerializeField] private LayerMask enemyLayer;
        [SerializeField] private Vector3 followOffset;
        
        [SerializeField] private float speed;
        [SerializeField] private float rotationSpeed;
        
        private InputReader _inputReader;
        private CmpBotModeManager _cmpBotModeManager;
        
        private GameObject _crosshair; 
        private GameObject _target;
        private Vector3 _firstFollowOffset;

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
            _firstFollowOffset = followOffset;
            _inputReader.InputActions.Player.CompanionMode.performed += OnCompanionModeChanged;
            _cmpBotModeManager.CurrentBotMode.SetProperties(eyeMaterial);
        }
        private void OnDisable()
        {
            _inputReader.InputActions.Player.CompanionMode.performed -= OnCompanionModeChanged;
        }

        private void OnCompanionModeChanged(InputAction.CallbackContext obj)
        {
            _cmpBotModeManager.NextMode();
            _cmpBotModeManager.CurrentBotMode.SetProperties(eyeMaterial);
            if (_cmpBotModeManager.CurrentBotMode is IAttackerCmp attackerCmp)
            {
               attackerCmp.SetAttackerProperties(transform,enemyLayer,damage,attackCooldown);
               attackerCmp.SetAttackEffect(attackEffect);
               followOffset.x = 0;
               followOffset.y = 2.5f;
               followOffset.z = 0;
            }
            else
            {
                followOffset = _firstFollowOffset;
            }
        }

        private void Update()
        {
            if (_target == null) return;
            _cmpBotModeManager.CurrentBotMode.Execute(_rotator,_crosshair, rotationSpeed);
        }
        private void FixedUpdate()
        {
            if (_target == null) return;
            _mover.FollowTargetWithTransformPosition(transform, _target.transform, speed * Time.fixedDeltaTime, followOffset);
        }
    }
}
