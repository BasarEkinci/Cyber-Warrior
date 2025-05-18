using CompanionBot.Mode;
using Inputs;
using Interfaces;
using Movement;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CompanionBot.Controller
{
    public class CmpManager : MonoBehaviour,IUpgradeable
    {
        [SerializeField] private ParticleSystem attackEffect;
        private CompanionBotSO _cmpSo;
        private InputReader _inputReader;
        private CmpBotModeManager _cmpBotModeManager;
        
        private GameObject _crosshair; 
        private GameObject _target;
        private Vector3 _followOffset;
        private Mover _mover;
        private Rotator _rotator;
    
        private int _cmpBotLevel = 0;
        
        private void Awake()
        {
            _target = GameObject.FindWithTag("Player");
            _crosshair = GameObject.FindWithTag("Crosshair");
            _cmpSo = Resources.Load<CompanionBotSO>("UnityObjects/Characters/CmpBot/CmpBot_" + _cmpBotLevel);
            _cmpBotModeManager = new CmpBotModeManager();
            _mover = new Mover(transform, _cmpSo.moveSpeed);
            _rotator = new Rotator(transform, _target);
            _inputReader = new InputReader();
        }

        private void OnEnable()
        {
            _inputReader.InputActions.Player.CompanionMode.performed += OnCompanionModeChanged;
            _cmpBotModeManager.CurrentBotMode.SetProperties(_cmpSo.eyeMaterial);
            _followOffset = _cmpSo.followOffset;
        }
        private void OnDisable()
        {
            _inputReader.InputActions.Player.CompanionMode.performed -= OnCompanionModeChanged;
        }

        private void OnCompanionModeChanged(InputAction.CallbackContext obj)
        {
            _cmpBotModeManager.NextMode();
            _cmpBotModeManager.CurrentBotMode.SetProperties(_cmpSo.eyeMaterial);
            if (_cmpBotModeManager.CurrentBotMode is IAttackerCmp attackerCmp)
            {
               attackerCmp.SetAttackerProperties(transform,_cmpSo.enemyLayer,_cmpSo.damage,_cmpSo.attackCooldown);
               attackerCmp.SetAttackEffect(attackEffect);
               _followOffset = _cmpSo.attackOffset;
            }
            else
            {
                _followOffset = _cmpSo.followOffset;
            }
        }

        private void Update()
        {
            if (_target == null) return;
            _cmpBotModeManager.CurrentBotMode.Execute(_rotator,_crosshair, _cmpSo.rotationSpeed);
        }
        private void FixedUpdate()
        {
            if (_target == null) return;
            _mover.FollowTargetWithTransformPosition(transform, _target.transform, _cmpSo.moveSpeed * Time.fixedDeltaTime, _followOffset);
        }

        public void Upgrade(float amount)
        {
            _cmpBotLevel += Mathf.RoundToInt(amount);
            _cmpSo = Resources.Load<CompanionBotSO>("UnityObjects/Characters/CmpBot/CmpBot_" + _cmpBotLevel);
        }
    }
}
