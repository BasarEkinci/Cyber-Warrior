using Data.UnityObjects;
using Interfaces;
using Managers;
using UnityEngine;

namespace UpgradeSystem
{
    public class WeaponUpgradeHandler : MonoBehaviour, IUpgradeable
    {
        [SerializeField] private PlayerGunStatsSO weaponData;
        [SerializeField] private LevelManager levelManager;
        [SerializeField] private string weaponID = "weapon_1";

        public int CurrentLevel => levelManager.CurrentLevel;
        public int MaxLevel => weaponData.MaxLevel;

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