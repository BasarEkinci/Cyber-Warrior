using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Enums;
using Runtime.Data.ValueObjects;
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
        
        [Header("Scriptables")]
        [SerializeField] private TransformEventChannel transformEvent;

        private CmpBotStatData _botData;
        private UpgradeItemType _upgradeItemType;
        private Transform _waitPointTransform;
        private Transform _parent;
        private CmpBotVFXPlayer _vfxPlayer;
        private PanelScreenBase _previousScreen;
        private List<PanelScreenBase> _screens = new();

        private void Awake()
        {
            _parent = transform.parent;
            _screens.Add(gunStatsScreen);
            _screens.Add(playerStatsScreen);
            _screens.Add(botStatsScreen);
        }

        private async void Start()
        {
            await UniTask.Yield();
            Initialize();
        }

        public override void Initialize()
        {
            transformEvent.OnEventRaised += OnTransformChanged;
            
            if (_vfxPlayer == null)
                _vfxPlayer = _parent.GetComponentInChildren<CmpBotVFXPlayer>(true);
            
            if (TargetObject == null)
                TargetObject = anchorPoints.GetInitialTargetObject();
            
            if (FollowPosition == null)
                FollowPosition = anchorPoints.GetAnchorPoint(mode);
            
            _botData = GetDataAtCurrentLevel();
            eyeMaterial.color = modeColor;
        }
        public override void Execute()
        {
        }
        public override void RotateBehaviour(Transform currentTransform)
        {
            currentTransform.LookAt(TargetObject);
        }

        public override void Move(Transform currentTransform,float deltaTime)
        {
            Vector3 desiredPosition = FollowPosition.position;
            currentTransform.position = Vector3.Lerp(currentTransform.position, desiredPosition,
                (botData.movementData.moveSpeed * deltaTime) / 2);
        }
        public override CmpBotStatData GetDataAtCurrentLevel()
        {
            return botData.statDataList[levelManager.CurrentLevel];
        }
        public override void ExitState()
        {
            transformEvent.OnEventRaised -= OnTransformChanged;
            foreach (var screen in _screens)
            {
                Debug.Log(screen.transform.name);
                screen.ClosePanel();
            }
        }
        private void OnTransformChanged(Transform val = null)
        {
            if (val == null)
            {
                TargetObject = anchorPoints.GetInitialTargetObject();
                FollowPosition = anchorPoints.GetAnchorPoint(mode);
                _previousScreen?.ClosePanel();
                _vfxPlayer.CloseLights();
                return;
            }
            _upgradeItemType = val.GetComponentInParent<UpgradeArea>().ItemType;
            TargetObject = anchorPoints.transform;
            _waitPointTransform = val;
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
    }
}