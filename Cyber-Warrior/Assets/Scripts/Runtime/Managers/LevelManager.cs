using Runtime.Enums;
using UnityEngine;

namespace Runtime.Managers
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private ItemType itemType;
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