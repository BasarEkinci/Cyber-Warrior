using Interfaces;
using ScriptableObjects.Events;
using UnityEngine;

namespace Economy
{
    public class UpgradeArea : MonoBehaviour,IInteractable
    {
        [SerializeField] private int upgradeCost;
        [SerializeField] private VoidEventSO upgradeEvent;
        private ScrapAmountController _scrapAmountController;
        private void Awake()
        {
            _scrapAmountController = GameObject.Find("ScrapAmountController").GetComponent<ScrapAmountController>();
        }

        public void Interact()
        {
            if (_scrapAmountController.ScarpAmount > 0)
            {
                _scrapAmountController.SpendScarp(upgradeCost);
                upgradeEvent.Invoke();
            }
        }
    }
}
