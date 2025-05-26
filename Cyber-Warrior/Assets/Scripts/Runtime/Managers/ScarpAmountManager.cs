using UnityEngine;
using UnityEngine.Events;

namespace Data.UnityObjects
{
    public class ScarpAmountManager : MonoBehaviour
    {
        public static ScarpAmountManager Instance { get; private set; }
        public int currentScarp => _currentScrap;
        
        public UnityAction<int> OnScrapEarned;
        public UnityAction<int> onScrapSpend;

        private int _currentScrap;
        
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void AddScarp(int amount)
        {
            _currentScrap += amount;
            OnScrapEarned?.Invoke(amount);
        }

        public bool TrySpendScarp(int amount)
        {
            if (_currentScrap < amount)
            {
                return false;
            }
            _currentScrap -= amount;
            onScrapSpend?.Invoke(amount);
            return true;
        }
    }
}
