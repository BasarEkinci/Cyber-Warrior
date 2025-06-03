using System;
using Runtime.Enums;
using Runtime.Interfaces;
using UnityEngine;

namespace Runtime.Managers
{
    public class LevelManager : MonoBehaviour,ISaveable
    {
        public string SaveId { get; private set; }
        
        [SerializeField] private string saveId;
        [SerializeField] private ItemType itemType;
        public int CurrentLevel => _currentLevel;

        private int _currentLevel = 0;

        private void Awake()
        {
            SaveId = saveId;
        }

        public void SetLevel(int level)
        {
            _currentLevel = level;
        }
        public void Upgrade()
        {
            _currentLevel++;
        }


        public void Save()
        {
        }

        public void Load()
        {
        }
    }
}