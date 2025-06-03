using Runtime.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime.Managers
{
    public class ScarpAmountManager : MonoBehaviour,ISaveable
    {
        public string SaveId { get; private set; }
        public static ScarpAmountManager Instance { get; private set; }
        public int CurrentScarp => _currentScrap;
        
        [SerializeField] private string saveId;
        
        public UnityAction<int> OnScrapEarned;
        public UnityAction<int> OnScrapSpend;

        private int _currentScrap;
        
        private void Awake()
        {
            SaveId = saveId;
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
            OnScrapSpend?.Invoke(amount);
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
