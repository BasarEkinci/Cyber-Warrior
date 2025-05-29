using Runtime.Enums;
using UnityEngine;

namespace Runtime.Managers
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private UpgradeItemType upgradeItemType;
        public int CurrentLevel => _currentLevel;

        private int _currentLevel;
        
        public void SetLevel(int level)
        {
            _currentLevel = level;
        }
        public void Upgrade()
        {
            _currentLevel++;
        }
    }
}