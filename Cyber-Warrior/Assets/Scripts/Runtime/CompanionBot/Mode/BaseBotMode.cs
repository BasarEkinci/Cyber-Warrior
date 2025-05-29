using System.Collections.Generic;
using Runtime.Data.UnityObjects.Events;
using Runtime.Enums;
using Runtime.Inputs;
using Runtime.Objects;
using Runtime.Objects.ControlPanelScreens;
using UnityEngine;

namespace Runtime.CompanionBot.Mode
{
    public class BaseBotMode : CmpBotMode
    {
        public override GameState ValidGameState => GameState.Base;
        public override Transform TargetObject { get; set; }
        public override Transform FollowPosition { get; set; }

        [Header("Stats Panels")] 
        [SerializeField] private PanelScreenBase gunStatsScreen;
        [SerializeField] private PanelScreenBase playerStatsScreen;
        [SerializeField] private PanelScreenBase botStatsScreen;
        [SerializeField] private PanelScreenBase gameStatsScreen;
        
        [Header("Scriptables")]
        [SerializeField] private TransformEventChannel transformEvent;
        
        private UpgradeItemType _upgradeItemType;
        private InputReader _inputReader;
        private Transform _waitPointTransform;
        private Transform _parent;
        private CmpBotVFXPlayer _vfxPlayer;
        private PanelScreenBase _previousScreen;
        private List<PanelScreenBase> _screens = new();

        private bool _isPlayerInUpgradeArea;
        private bool _isStatsPanelOpened;
        

        #region Unity Functions

        private void Awake()
        {
            _parent = transform.parent;
            _inputReader = GetComponentInParent<CmpBotModeManager>().InputReader;
            _screens.Add(gunStatsScreen);
            _screens.Add(playerStatsScreen);
            _screens.Add(botStatsScreen);
        }
        #endregion

        #region Overridden Functions

        public override void Initialize()
        {
            transformEvent.OnEventRaised += HandleTransformChanged;
            _inputReader.OnStatsButtonPressed += OnStatsButtonPressed;
            
            if (_vfxPlayer == null)
                _vfxPlayer = _parent.GetComponentInChildren<CmpBotVFXPlayer>(true);
            
            if (TargetObject == null)
                TargetObject = anchorPoints.GetInitialTargetObject();
            
            if (FollowPosition == null)
                FollowPosition = anchorPoints.GetAnchorPoint(mode);
            
            eyeMaterial.color = modeColor;
        }

        private void OnStatsButtonPressed()
        {
            if (_isPlayerInUpgradeArea)
            {
                return;
            }
            if (_isStatsPanelOpened)
            {
                gameStatsScreen.ClosePanel();
                _isStatsPanelOpened = false;
                TargetObject = anchorPoints.GetInitialTargetObject();
                _vfxPlayer.CloseLights();
                return;
            }
            _vfxPlayer.OpenLights();
            TargetObject = Camera.main.transform;
            gameStatsScreen.OpenPanel();
            _isStatsPanelOpened = true;
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
            foreach (var screen in _screens)
            {
                screen.ClosePanel();
            }
        }

        #endregion

        #region Custom Functions

        private void HandleTransformChanged(Transform changedTransform = null)
        {
            if (_vfxPlayer == null)
                _vfxPlayer = _parent.GetComponentInChildren<CmpBotVFXPlayer>(true);
            
            if (changedTransform == null)
            {
                TargetObject = anchorPoints.GetInitialTargetObject();
                FollowPosition = anchorPoints.GetAnchorPoint(mode);
                _previousScreen?.ClosePanel();
                _vfxPlayer.CloseLights();
                _isPlayerInUpgradeArea = false;
                return;
            }
            _isStatsPanelOpened = false;
            gameStatsScreen.ClosePanel();
            _isPlayerInUpgradeArea = true;
            _upgradeItemType = changedTransform.GetComponentInParent<UpgradeArea>().ItemType;
            TargetObject = anchorPoints.transform;
            _waitPointTransform = changedTransform;
            FollowPosition = _waitPointTransform;
            OpenCurrentItemStatsScreen();
        }

        private void OpenCurrentItemStatsScreen()
        {
            switch (_upgradeItemType)
            {
                case UpgradeItemType.Player:
                    _previousScreen = playerStatsScreen;
                    _vfxPlayer.OpenLights();
                    _previousScreen.OpenPanel();
                    break;
                case UpgradeItemType.Companion:
                    _previousScreen = botStatsScreen;
                    _vfxPlayer.OpenLights();
                    _previousScreen.OpenPanel();
                    break;
                case UpgradeItemType.Gun:
                    _previousScreen = gunStatsScreen;
                    _vfxPlayer.OpenLights();
                    _previousScreen.OpenPanel();
                    break;
            }
        }

        #endregion
    }
}
