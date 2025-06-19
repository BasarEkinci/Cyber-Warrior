using Runtime.Data.UnityObjects.Events;
using Runtime.Extensions;
using UnityEngine;

namespace Runtime.Managers
{
    public class ScarpAmountManager : MonoSingleton<ScarpAmountManager>
    {
        public IntegerEventSo OnScarpSpend => scrapSpendEvent;
        public IntegerEventSo OnScarpEarned => scrapEarnedEvent;
        public int CurrentScarp => _currentScrap;
        
        [SerializeField] private IntegerEventSo scrapEarnedEvent;
        [SerializeField] private IntegerEventSo scrapSpendEvent;

        private int _currentScrap;

        private void OnEnable()
        {
            _currentScrap = GameDatabaseManager.Instance.LoadData(SaveKeys.PlayerLevel);
        }

        public void AddScarp(int amount)
        {
            _currentScrap = GameDatabaseManager.Instance.LoadData(SaveKeys.PlayerLevel);
            _currentScrap += amount;
            scrapEarnedEvent.OnEventRaised(amount);
            GameDatabaseManager.Instance.SaveData(SaveKeys.ScrapAmount, _currentScrap);
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
    }
}
