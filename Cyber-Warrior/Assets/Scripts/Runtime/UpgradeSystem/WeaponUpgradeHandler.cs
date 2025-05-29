using Data.UnityObjects;
using Runtime.Interfaces;
using Runtime.Managers;
using UnityEngine;

namespace Runtime.UpgradeSystem
{
    public class WeaponUpgradeHandler : MonoBehaviour, IUpgradeable
    {
        [SerializeField] private PlayerGunStatsSO weaponData;
        [SerializeField] private LevelManager levelManager;
        [SerializeField] private string weaponID = "weapon_1";

        public int CurrentLevel { get; set; }
        public int MaxLevel { get; set; }

        private void OnEnable()
        {
            CurrentLevel = levelManager.CurrentLevel;
            MaxLevel = weaponData.MaxLevel;
        }

        public int GetLevelPrice(int level) => weaponData.GunStatsList[level].price;

        public void Upgrade()
        {
            levelManager.Upgrade();
            SaveManager.Instance.CurrentData.WeaponLevels[weaponID] = levelManager.CurrentLevel;
            SaveManager.Instance.Save();
            Debug.Log($"Weapon upgraded to level {levelManager.CurrentLevel}");
        }
    }
}