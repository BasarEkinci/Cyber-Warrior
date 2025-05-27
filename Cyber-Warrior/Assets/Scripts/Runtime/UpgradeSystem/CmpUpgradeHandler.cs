using Data.UnityObjects;
using Interfaces;
using Managers;
using UnityEngine;

namespace UpgradeSystem
{
    public class CmpUpgradeHandler : MonoBehaviour, IUpgradeable
    {
        [SerializeField] private CmpBotDataSO data;
        [SerializeField] private LevelManager levelManager;
        [SerializeField] private string companionID = "companion_1";
        
        public int CurrentLevel { get; }
        public int MaxLevel { get; }
        
        public int GetLevelPrice(int level) => data.statDataList[level].levelPrice;

        public void Upgrade()
        {
            levelManager.Upgrade();
            SaveManager.Instance.CurrentData.CompanionLevels[companionID] = levelManager.CurrentLevel;
            SaveManager.Instance.Save();
            Debug.Log($"Companion upgraded to level {levelManager.CurrentLevel}");
        }
    }
}