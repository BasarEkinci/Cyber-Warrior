using UnityEngine;
using UnityEngine.Events;

namespace Data.UnityObjects
{
    public class ScarpAmountManager : MonoBehaviour
    {
        public static ScarpAmountManager Instance { get; private set; }

        [Header("Scrap Info")]
        public int currentScarp;
        public UnityEvent onScrapAmountChanged;

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
            currentScarp += amount;
            onScrapAmountChanged?.Invoke();
        }

        public bool TrySpendScarp(int amount)
        {
            if (currentScarp < amount)
            {
                return false;
            }

            currentScarp -= amount;
            onScrapAmountChanged?.Invoke();
            return true;
        }
    }
}
