using UnityEngine;
using UnityEngine.Events;

namespace Data.UnityObjects
{
    [CreateAssetMenu(fileName = "ScarpData", menuName = "Scriptable Objects/ScarpData")]
    public class ScrapDataSO : ScriptableObject
    {
        public int currentScarp;

        public UnityAction OnScrapAmountChanged;
        
        public void AddScarp(int amount)
        {
            currentScarp += amount;
            OnScrapAmountChanged?.Invoke();
        }
        
        public bool TrySpendScarp(int amount)
        {
            if (currentScarp < amount)
            {
                return false;
            }
            currentScarp -= amount;
            OnScrapAmountChanged?.Invoke();
            return true;
        }
    }
}
