using Managers;
using Runtime.Data.UnityObjects.ObjectData;
using Runtime.Interfaces;
using UnityEngine;

namespace Runtime.UpgradeSystem
{
    public class CmpUpgradeHandler : MonoBehaviour, IUpgradeable
    {
        [SerializeField] private CmpBotDataSO data;
        [SerializeField] private LevelManager levelManager;
        [SerializeField] private string companionID = "companion_1";

        private GameObject _currentLevelMesh;
        public int CurrentLevel { get; set; }
        public int MaxLevel { get; set; }
        public int GetLevelPrice(int level) => data.statDataList[level].levelPrice;

        private void OnEnable()
        {
            MaxLevel = data.MaxLevel;
            CurrentLevel = levelManager.CurrentLevel;
            Debug.Log(CurrentLevel);
            _currentLevelMesh = Instantiate(data.statDataList[CurrentLevel].visualData.mesh,transform.position,Quaternion.identity, transform);
        }

        public void Upgrade()
        {
            Destroy(_currentLevelMesh);
            levelManager.Upgrade();
            _currentLevelMesh = Instantiate(data.statDataList[levelManager.CurrentLevel].visualData.mesh, transform);
            //SaveManager.Instance.CurrentData.CompanionLevels[companionID] = levelManager.CurrentLevel;
            //SaveManager.Instance.Save();
            Debug.Log($"Companion upgraded to level {levelManager.CurrentLevel}");
        }
    }
}