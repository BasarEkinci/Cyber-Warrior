using Data.UnityObjects;
using Interfaces;
using Managers;
using UnityEngine;

namespace UpgradeSystem
{
    public class PlayerUpgradeHandler : MonoBehaviour, IUpgradeable
    {
        [SerializeField] private PlayerStatsSO playerData;
        [SerializeField] private LevelManager levelManager;
        [SerializeField] private string playerID = "player_1";

        public int CurrentLevel => levelManager.CurrentLevel;
        public int MaxLevel => playerData.MaxLevel;

        public int GetLevelPrice(int level) => playerData.playerStatsDataList[level].levelPrice;

        public void Upgrade()
        {
            levelManager.Upgrade();
            SaveManager.Instance.CurrentData.PlayerLevels[playerID] = levelManager.CurrentLevel;
            SaveManager.Instance.Save();
            Debug.Log($"Player upgraded to level {levelManager.CurrentLevel}");
        }
    }
}