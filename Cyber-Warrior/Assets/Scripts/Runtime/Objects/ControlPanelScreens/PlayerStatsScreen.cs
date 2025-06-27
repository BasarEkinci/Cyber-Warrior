using DG.Tweening;
using Runtime.Data.UnityObjects.Events;
using Runtime.Data.UnityObjects.ObjectData;
using Runtime.Data.ValueObjects;
using Runtime.Managers;
using TMPro;
using UnityEngine;

namespace Runtime.Objects.ControlPanelScreens
{
    public class PlayerStatsScreen : PanelScreenBase
    {
        public override bool IsPanelActive { get; set; }
        
        [Header("Data")]
        [SerializeField] private PlayerStatsSO statsData;
        [SerializeField] private VoidEventSO upgradeSucceedEvent;

        [Header("Text")] 
        [SerializeField] private TMP_Text maxHealth;
        [SerializeField] private TMP_Text level;

        private PlayerStatsData _data;

        private void Start()
        {
            ClosePanel();
        }

        private void OnEnable()
        {
            upgradeSucceedEvent.OnEventRaised += OnUpgradeSucceed;
            SetStatsToScreen();
        }

        private void OnUpgradeSucceed()
        {
            transform.DOScale(transform.localScale * 1.5f, 0.1f).SetLoops(2, LoopType.Yoyo);
            SetStatsToScreen();
        }

        private void OnDisable()
        {
            upgradeSucceedEvent.OnEventRaised -= OnUpgradeSucceed;
        }

        public override void SetStatsToScreen()
        {
            _data = statsData.playerStatsDataList[GameDatabaseManager.Instance.LoadData(SaveKeys.PlayerLevel)];
            maxHealth.text = "Max Health: " + _data.maxHealth;
            level.text = "Level: " + (GameDatabaseManager.Instance.LoadData(SaveKeys.PlayerLevel) + 1);
        }

        public override void OpenPanel()
        {
            IsPanelActive = true;
            transform.DOScale(Vector3.one, 0.1f);
        }

        public override void ClosePanel()
        {
            IsPanelActive = false;
            transform.DOScale(Vector3.zero, 0.1f);
        }
    }
}