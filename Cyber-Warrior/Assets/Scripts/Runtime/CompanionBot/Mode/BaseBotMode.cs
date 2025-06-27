using System;
using Runtime.Data.UnityObjects.Events;
using Runtime.Enums;
using Runtime.Inputs;
using Runtime.Managers;
using Runtime.Objects;
using UnityEngine;


namespace Runtime.CompanionBot.Mode
{
    public class BaseBotMode : CmpBotMode
    {
        public override GameState ValidGameState => GameState.Base;
        public override Transform TargetObject { get; set; }
        public override Transform FollowPosition { get; set; }
        
        [Header("Scriptables")]
        [SerializeField] private TransformEventChannel transformEvent;
        
        [Header("Class References")]
        [SerializeField] private InputReader inputReader;

        private PanelManager _panelManager;
        private BotAnchorPoints _targetProvider;
        
        private bool _isStateActive;

        private void Awake()
        {
            _targetProvider = FindFirstObjectByType<BotAnchorPoints>();
            _panelManager = GetComponentInParent<PanelManager>();
        }
        private void Start()
        {
            InitializeComponents();
            TargetObject = _targetProvider.GetInitialTargetObject();
            FollowPosition = _targetProvider.GetAnchorPoint(mode);
            eyeMaterial.color = modeColor;
        }
        #region Overridden Functions

        // ReSharper disable Unity.PerformanceAnalysis
        public override void Initialize()
        {   
            transformEvent.OnEventRaised += HandleTransformChanged;
            inputReader.OnStatsButtonPressed += OnStatsButtonPressed;
            InitializeComponents();
            _isStateActive = true;
            eyeMaterial.color = modeColor;
        }

        public override void Execute()
        {
        }

        public override void RotateBehaviour(Transform botTransform)
        {
            botTransform.LookAt(TargetObject);
        }

        public override void Move(Transform botTransform, float deltaTime)
        {
            Vector3 desiredPosition = FollowPosition.position;
            botTransform.position = Vector3.Lerp(botTransform.position, desiredPosition,
                (botData.movementData.moveSpeed * deltaTime) / 2);
        }

        public override void ExitState()
        {
            transformEvent.OnEventRaised -= HandleTransformChanged;
            inputReader.OnStatsButtonPressed -= OnStatsButtonPressed;
            _panelManager.CloseAllPanels();
            _isStateActive = false;
        }

        private void OnStatsButtonPressed()
        {
            if (!_isStateActive || TargetObject == anchorPoints.transform)
            {
                return;
            }
            if (!_panelManager.IsStatsPanelActive)
            {
                _panelManager.OpenGameStatsPanel();
                TargetObject = Camera.main.transform;
                return;
            }
            _panelManager.CloseGameStatsPanel();
            TargetObject = _targetProvider.GetInitialTargetObject();
        }

        #endregion

        #region Class Functions

        private void InitializeComponents()
        {
            if (_targetProvider == null)
                _targetProvider = FindFirstObjectByType<BotAnchorPoints>();
            if (inputReader == null)
                inputReader = FindFirstObjectByType<InputReader>();
            if (_panelManager == null)
                _panelManager = GetComponentInParent<PanelManager>();
            if (TargetObject == null)
                TargetObject = anchorPoints.GetInitialTargetObject();
            if (FollowPosition == null)
                FollowPosition = anchorPoints.GetAnchorPoint(mode);
        }
        
        private void HandleTransformChanged(Transform changedTransform = null)
        {
            if (changedTransform == null)
            {
                TargetObject = _targetProvider.GetInitialTargetObject();
                FollowPosition = _targetProvider.GetAnchorPoint(mode);
                _panelManager.CloseAllPanels();
                return;
            }
            _panelManager.CloseGameStatsPanel();
            var upgradeArea = changedTransform.GetComponentInParent<UpgradeArea>();
            if (upgradeArea != null)
            {
                _panelManager.OpenPanel(upgradeArea.Type);
            }
            TargetObject = anchorPoints.transform;
            FollowPosition = changedTransform;
        }
        #endregion
    }
}
