using Runtime.Data.UnityObjects.Events;
using Runtime.Extensions;
using Runtime.Interfaces;
using UnityEngine;

namespace Runtime.Managers
{
    public class ScarpAmountManager : MonoSingleton<ScarpAmountManager>,ISaveable
    {
        public IntegerEventSo OnScarpSpend => scrapSpendEvent;
        public IntegerEventSo OnScarpEarned => scrapEarnedEvent;
        public string SaveId { get; private set; }
        public int CurrentScarp => _currentScrap;
        
        [SerializeField] private string saveId;
        [SerializeField] private IntegerEventSo scrapEarnedEvent;
        [SerializeField] private IntegerEventSo scrapSpendEvent;

        private int _currentScrap;
        public void AddScarp(int amount)
        {
            _currentScrap += amount;
            scrapEarnedEvent.OnEventRaised(amount);
        }

        public bool TrySpendScarp(int amount)
        {
            if (_currentScrap < amount)
            {
                return false;
            }
            _currentScrap -= amount;
            scrapSpendEvent.OnEventRaised(amount);
            return true;
        }

        public void Save()
        {
        }

        public void Load()
        {
        }
    }
}
