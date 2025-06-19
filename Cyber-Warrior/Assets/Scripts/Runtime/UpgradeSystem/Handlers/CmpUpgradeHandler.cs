using Runtime.Data.UnityObjects.ObjectData;
using Runtime.Interfaces;
using Runtime.Managers;
using UnityEngine;

namespace Runtime.UpgradeSystem.Handlers
{
    public class CmpUpgradeHandler : MonoBehaviour,IUpgradeable
    {
        [SerializeField] private CmpBotDataSo data;
        public int CurrentLevel { get; set; }
        public int MaxLevel { get; set; }

        private GameObject _currentMesh;
        
        private void Awake()
        {
            MaxLevel = data.statDataList.Count - 1;
        }

        private void OnEnable()
        {
            CurrentLevel = GameDatabaseManager.Instance.LoadData(SaveKeys.CompanionLevel);
            _currentMesh = Instantiate(data.statDataList[CurrentLevel].visualData.mesh, transform);
        }

        public int GetLevelPrice(int level)
        {
            return data.statDataList[CurrentLevel].levelPrice;
        }

        public void Upgrade()
        {
            if (CurrentLevel == MaxLevel)
            {
                Debug.Log("Already at max level");
                return;
            }
            if (ScarpAmountManager.Instance.CurrentScarp < GetLevelPrice(CurrentLevel))
            {
                Debug.Log("Not enough scarp to upgrade");
                return;
            }
            ScarpAmountManager.Instance.OnScarpSpend.OnEventRaised(GetLevelPrice(CurrentLevel));
            Debug.Log($"Upgrading Cmp Bot to level {CurrentLevel + 1}");
            Destroy(_currentMesh);
            CurrentLevel++;
            GameDatabaseManager.Instance.SaveData(SaveKeys.CompanionLevel, CurrentLevel);
            _currentMesh = Instantiate(data.statDataList[CurrentLevel].visualData.mesh, transform);
        }
    }
}