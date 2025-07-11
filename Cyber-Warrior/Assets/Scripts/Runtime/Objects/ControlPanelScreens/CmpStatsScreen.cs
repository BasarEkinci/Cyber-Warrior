﻿using DG.Tweening;
using Runtime.Data.UnityObjects.Events;
using Runtime.Data.UnityObjects.ObjectData;
using Runtime.Data.ValueObjects;
using Runtime.Managers;
using TMPro;
using UnityEngine;

namespace Runtime.Objects.ControlPanelScreens
{
    public class CmpStatsScreen : PanelScreenBase
    {
        [SerializeField] private VoidEventSO upgradeSucceedEvent;
        [Header("Texts")] 
        [SerializeField] private TMP_Text healAmountText;
        [SerializeField] private TMP_Text healRateText;
        [SerializeField] private TMP_Text damageAmountText;
        [SerializeField] private TMP_Text attackRateText;
        [SerializeField] private TMP_Text nextLevelCost;
        [SerializeField] private TMP_Text rangeText;
        
        [Header("Visual Settings")]
        [SerializeField] private float scaleFactor = 1f;
        
        [Header("Data")]
        [SerializeField] private CmpBotDataSo botDataSo;

        private CmpHealerData _healerData;
        private CmpCombatData _combatData;

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
            Debug.Log("Upgrade Succeed");
            SetStatsToScreen();
            transform.DOScale(transform.localScale * 1.5f, 0.1f).SetLoops(2, LoopType.Yoyo);
        }

        public override bool IsPanelActive { get; set; }

        public override void SetStatsToScreen()
        {
            int dataLevel = GameDatabaseManager.Instance.LoadData(SaveKeys.CompanionLevel);
            _healerData = botDataSo.statDataList[dataLevel].healerData;
            _combatData = botDataSo.statDataList[dataLevel].combatData;
            healAmountText.text =
                "Heal Amount: " + _healerData.healAmount;
            healRateText.text = "Heal Rate: " + _healerData.healCooldown;
            damageAmountText.text = "Damage: " + _combatData.damage;
            attackRateText.text = "Attack Rate: " + _combatData.attackCooldown;
            rangeText.text = "Range: " + _combatData.range;
            nextLevelCost.text = "Upgrade Cost: " + botDataSo.statDataList[dataLevel].levelPrice;
        }

        public override void OpenPanel()
        {
            IsPanelActive = true;
            transform.DOScale(Vector3.one * scaleFactor, 0.1f);
        }

        public override void ClosePanel()
        {
            IsPanelActive = false;
            transform.DOScale(Vector3.zero, 0.1f);
        }
    }
}