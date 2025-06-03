using Data.UnityObjects;
using Runtime.Interfaces;
using Runtime.Managers;
using UnityEngine;

namespace Runtime.UpgradeSystem
{
    public class PlayerUpgradeHandler : MonoBehaviour, IUpgradeable
    {
        [SerializeField] private PlayerStatsSO playerData;
        [SerializeField] private LevelManager levelManager;
        [SerializeField] private string playerID = "player_1";

        public int CurrentLevel { get; set; }
        public int MaxLevel { get; set; }

        private void OnEnable()
        {
            CurrentLevel = levelManager.CurrentLevel;
            MaxLevel = playerData.MaxLevel;
        }

        public int GetLevelPrice(int level) => playerData.playerStatsDataList[level].levelPrice;

        public void Upgrade()
        {
            levelManager.Upgrade();
            Debug.Log($"Player upgraded to level {levelManager.CurrentLevel}");
        }
    }
}