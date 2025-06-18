using Runtime.Data.UnityObjects.ObjectData;
using Runtime.Interfaces;
using Runtime.Managers;
using UnityEngine;

namespace Runtime.UpgradeSystem.Handlers
{
    public class PlayerUpgradeHandler : MonoBehaviour,IUpgradeable
    {
        [SerializeField] private PlayerStatsSO data;
        public int CurrentLevel { get; set; }
        public int MaxLevel { get; set; }
        public int GetLevelPrice(int level)
        {
            return data.playerStatsDataList[level].levelPrice;
        }

        public void Upgrade()
        {
            if (ScarpAmountManager.Instance.CurrentScarp < GetLevelPrice(CurrentLevel))
            {
                Debug.Log("Not enough scarp to upgrade");
                return;
            }
            ScarpAmountManager.Instance.OnScarpSpend.OnEventRaised(GetLevelPrice(CurrentLevel));
            Debug.Log($"Upgrading Player to level {CurrentLevel + 1}");
            CurrentLevel++;
        }
    }
}