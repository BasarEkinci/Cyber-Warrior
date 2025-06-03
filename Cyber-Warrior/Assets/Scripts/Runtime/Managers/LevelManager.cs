using Runtime.Enums;
using UnityEngine;
using UnityEngine.Serialization;

namespace Runtime.Managers
{
    public class LevelManager : MonoBehaviour
    {
        [FormerlySerializedAs("upgradeItemType")] [SerializeField] private ItemType itemType;
        public int CurrentLevel => _currentLevel;

        private int _currentLevel = 0;
        
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