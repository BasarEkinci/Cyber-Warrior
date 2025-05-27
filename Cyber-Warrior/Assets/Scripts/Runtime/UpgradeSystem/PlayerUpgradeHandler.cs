using System;
using Data.UnityObjects;
using Managers;
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
            SaveManager.Instance.CurrentData.PlayerLevels[playerID] = levelManager.CurrentLevel;
            SaveManager.Instance.Save();
            Debug.Log($"Player upgraded to level {levelManager.CurrentLevel}");
        }
    }
}