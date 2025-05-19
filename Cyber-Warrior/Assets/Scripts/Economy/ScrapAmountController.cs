using ScriptableObjects.Events;
using TMPro;
using UnityEngine;

namespace Economy
{
    public class ScrapAmountController : MonoBehaviour
    {
        public int ScarpAmount => _scrapAmount;
        [SerializeField] private VoidEventSO scrapCollectEvent;
        
        private int _scrapAmount;
        
        private void OnEnable()
        {
            scrapCollectEvent.OnEventRaised += OnCollectScarp;
        }
        
        private void OnDisable()
        {
            scrapCollectEvent.OnEventRaised -= OnCollectScarp;
        }
        
        private void OnCollectScarp()
        {
            _scrapAmount++;
        }

        public void SpendScarp(int amount)
        {
            if (_scrapAmount <= 0)
            {
                return;
            }
            if (_scrapAmount >= amount)
            {
                _scrapAmount -= amount;
            }
            else
            {
                Debug.LogWarning("Not enough scrap!");
            }
        }
    }
}
