using Runtime.Data.UnityObjects.ObjectData;
using Runtime.Interfaces;
using Runtime.Managers;
using UnityEngine;

namespace Runtime.UpgradeSystem.Handlers
{
    public class GunUpgradeHandler : MonoBehaviour, IUpgradeable
    {
        [SerializeField] private PlayerGunStatsSO data;
        public int CurrentLevel { get; set; }
        public int MaxLevel { get; set; }

        private void OnEnable()
        {
            CurrentLevel = GameDatabaseManager.Instance.LoadData(SaveKeys.GunLevel);
            MaxLevel = data.GunStatsList.Count - 1;
        }

        public int GetLevelPrice(int level)
        {
            return data.GunStatsList[level].price;
        }

        public void Upgrade()
        {
            if (ScarpAmountManager.Instance.CurrentScarp < GetLevelPrice(CurrentLevel))
            {
                Debug.Log("Not enough scarp to upgrade");
                return;
            }
            ScarpAmountManager.Instance.OnScarpSpend.OnEventRaised(GetLevelPrice(CurrentLevel));
            Debug.Log($"Upgrading Gun to level {CurrentLevel + 1}");
            CurrentLevel++;
            GameDatabaseManager.Instance.SaveData(SaveKeys.GunLevel, CurrentLevel);
        }
    }
}