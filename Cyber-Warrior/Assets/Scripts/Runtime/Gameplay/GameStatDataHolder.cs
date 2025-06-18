using System;
using Runtime.Data.UnityObjects.Events;
using Runtime.Managers;
using UnityEngine;

namespace Runtime.Gameplay
{
    public class GameStatDataHolder : MonoBehaviour
    {
        public int TotalKills => _totalKill;
        public int CollectedScrap => _collectedScrap;
        
        [SerializeField] private int totalKills;
        [SerializeField] private int collectedScrap;
        [SerializeField] private VoidEventSO onEnemyKilledEvent;

        private int _totalKill;
        private int _collectedScrap;

        private void OnEnable()
        {
            _totalKill = totalKills;
            _collectedScrap = collectedScrap;
            ScarpAmountManager.Instance.OnScarpEarned.OnEventRaised += amount =>
            {
                _collectedScrap += amount;
            };
            onEnemyKilledEvent.OnEventRaised += () =>
            {
                _totalKill++;
            };
        }

        private void OnDisable()
        {
            ScarpAmountManager.Instance.OnScarpEarned.OnEventRaised -= amount =>
            {
                _collectedScrap += amount;
            };
            onEnemyKilledEvent.OnEventRaised -= () =>
            {
                _totalKill++;
            };
        }
    }
}