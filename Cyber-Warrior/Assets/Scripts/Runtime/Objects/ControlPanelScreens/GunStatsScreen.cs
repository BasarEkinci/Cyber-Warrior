using Data.UnityObjects;
using DG.Tweening;
using Runtime.Data.UnityObjects.Events;
using Runtime.Data.UnityObjects.ObjectData;
using Runtime.Data.ValueObjects;
using Runtime.Managers;
using TMPro;
using UnityEngine;

namespace Runtime.Objects.ControlPanelScreens
{
    public class GunStatsScreen : PanelScreenBase
    {
        public override bool IsPanelActive { get; set; }
        
        [Header("Data")]
        [SerializeField] private PlayerGunStatsSO gunStats;
        [SerializeField] private VoidEventSO upgradeSucceedEvent;
        
        [Header("Text")]
        [SerializeField] private TMP_Text damageText;
        [SerializeField] private TMP_Text rangeText;
        [SerializeField] private TMP_Text attackRateText;
        [SerializeField] private TMP_Text upgradeCostText;

        private GunStats _stats;

        private void OnEnable()
        {
            upgradeSucceedEvent.OnEventRaised += OnUpgradeSucceed;
            SetStatsToScreen();
        }
        
        private void OnDisable()
        {
            upgradeSucceedEvent.OnEventRaised -= OnUpgradeSucceed;
        }

        private void OnUpgradeSucceed()
        {
           
            SetStatsToScreen();
            transform.DOScale(transform.localScale * 1.5f, 0.1f).SetLoops(2, LoopType.Yoyo);
        }

        public override void SetStatsToScreen()
        {
            _stats = gunStats.GunStatsList[GameDatabaseManager.Instance.LoadData(SaveKeys.GunLevel)];
            damageText.text = "Damage: " + _stats.damage;
            rangeText.text = "Range: " + _stats.range;
            attackRateText.text = "Attack Rate: " + _stats.attackInterval;
            upgradeCostText.text = "Upgrade Cost: " + _stats.price;
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