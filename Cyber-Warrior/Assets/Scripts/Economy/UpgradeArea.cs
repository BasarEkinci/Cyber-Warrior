using Interfaces;
using ScriptableObjects;
using ScriptableObjects.Events;
using TMPro;
using UnityEngine;

namespace Economy
{
    public class UpgradeArea : MonoBehaviour,IInteractable
    {
        [SerializeField] private TMP_Text scarpAmountText;
        [SerializeField] private TMP_Text cmpBotCooldownText;
        [SerializeField] private TMP_Text cmpBotDamageText;
        [SerializeField] private TMP_Text cmpBotLevelText;
        [SerializeField] private VoidEventSO upgradeEvent;
        [SerializeField] private CmpBotLevelDataSO cmpBotLevelData;
        
        private ScrapAmountController _scrapAmountController;
        private CompanionBotSO _currentCmpBotStats;
        
        private int _cmpBotLevel = 0;
        private void Awake()
        {
            _scrapAmountController = GameObject.Find("ScrapAmountController").GetComponent<ScrapAmountController>();
            GetLevelStats();
        }

        private void OnEnable()
        {
            SetLevelStats();
        }

        public void Interact()
        {
            if (cmpBotLevelData.MaxLevel == _cmpBotLevel)
            {
                return;
            }
            if (_scrapAmountController.ScarpAmount > 0)
            {
                _scrapAmountController.SpendScarp(cmpBotLevelData.levelDataList[_cmpBotLevel].price);
                upgradeEvent.Invoke();
                scarpAmountText.text = ": " + _scrapAmountController.ScarpAmount;
                _cmpBotLevel++;
                GetLevelStats();
                SetLevelStats();
            }
        }

        private void GetLevelStats()
        {
            _currentCmpBotStats = Resources.Load<CompanionBotSO>($"UnityObjects/Characters/CmpBot/CmpBot_{_cmpBotLevel}");
        }

        private void SetLevelStats()
        {
            cmpBotLevelText.text = ": " + _cmpBotLevel + 1;
            cmpBotCooldownText.text = ": " + _currentCmpBotStats.attackCooldown.ToString("F");
            cmpBotDamageText.text = ": " + _currentCmpBotStats.damage;
            scarpAmountText.text = ": " + _scrapAmountController.ScarpAmount;
        }
    }
}
