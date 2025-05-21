using CompanionBot.Mode;
using Data.UnityObjects;
using Data.ValueObjects;
using Inputs;
using Movement;
using UnityEngine;

namespace Managers
{
    public class CmpManager : MonoBehaviour
    {
        [SerializeField] private CmpBotDataSO cmpDataSo;
        [SerializeField] private VoidEventSO onUpgradeEvent;
        [SerializeField] private ParticleSystem attackEffect;
        [SerializeField] private InputReader inputReader;
        
        private CmpBotModeManager _cmpBotModeManager;
        private LevelManager _levelManager;
        
        private GameObject _crosshair; 
        private GameObject _target;
        private Vector3 _followOffset;
        private Mover _mover;
        private Rotator _rotator;
        private CmpBotStatData _currentData;
        private void Awake()
        {
            _target = GameObject.FindWithTag("Player");
            _crosshair = GameObject.FindWithTag("Crosshair");
            _cmpBotModeManager = new CmpBotModeManager();
            _levelManager = GetComponent<LevelManager>();
            _mover = new Mover(transform, cmpDataSo.statDataList[_levelManager.CurrentLevel].CombatData.MoveSpeed);
            _rotator = new Rotator(transform, _target);
        }

        private void OnEnable()
        {
            _currentData = cmpDataSo.statDataList[_levelManager.CurrentLevel];
            onUpgradeEvent.OnEventRaised += Upgrade;
            inputReader.OnSwitchMode += OnCompanionModeChanged;
            _cmpBotModeManager.CurrentBotMode.SetProperties(_currentData.VisualData.EyeMaterial);
            _followOffset = _currentData.CombatData.FollowOffset;
        }
        private void OnDisable()
        {
            inputReader.OnSwitchMode -= OnCompanionModeChanged;
            onUpgradeEvent.OnEventRaised -= Upgrade;
            _currentData = cmpDataSo.statDataList[_levelManager.CurrentLevel];
            _cmpBotModeManager.CurrentBotMode.SetProperties(_currentData.VisualData.EyeMaterial);
        }

        private void OnCompanionModeChanged()
        {
            _currentData = cmpDataSo.statDataList[_levelManager.CurrentLevel];
            _cmpBotModeManager.NextMode();
            _cmpBotModeManager.CurrentBotMode.SetProperties(_currentData.VisualData.EyeMaterial);
            if (_cmpBotModeManager.CurrentBotMode is IAttackerCmp attackerCmp)
            {
               attackerCmp.SetAttackerProperties(transform,_currentData.CombatData.EnemyLayer,
                  _currentData.CombatData.Damage,_currentData.CombatData.AttackCooldown);
               attackerCmp.SetAttackEffect(attackEffect);
               _followOffset = _currentData.CombatData.AttackOffset;
            }
            else
            {
                _followOffset = _currentData.CombatData.FollowOffset;
                _cmpBotModeManager.CurrentBotMode.Execute(_rotator,_crosshair,_currentData.CombatData.RotationSpeed);
            }
        }

        private void Update()
        {
            if (_target == null) return;
            _cmpBotModeManager.CurrentBotMode.Execute(_rotator,_crosshair,_currentData.CombatData.RotationSpeed);
        }
        private void FixedUpdate()
        {
            if (_target == null) return;
            _mover.FollowTargetWithTransformPosition(transform, _target.transform, 
                _currentData.CombatData.MoveSpeed * Time.fixedDeltaTime, _followOffset);
        }

        public void Upgrade()
        {
            if (_levelManager.CurrentLevel >= cmpDataSo.MaxLevel) 
                return;
            _levelManager.Upgrade();
            Debug.Log("Companion Bot Upgraded to level: " + _levelManager.CurrentLevel);
            _currentData = cmpDataSo.statDataList[_levelManager.CurrentLevel];
        }
    }
}
