using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Runtime.Managers
{
    [System.Serializable]
    public class SaveData
    {
        public int totalScrap;
        public Dictionary<string, int> CompanionLevels = new();
        public Dictionary<string, int> WeaponLevels = new();
        public Dictionary<string, int> PlayerLevels = new();
    }
    
    public class SaveManager : MonoBehaviour
    {
        public static SaveManager Instance { get; private set; }
        public SaveData CurrentData { get; private set; }

        private string savePath;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                savePath = Application.persistentDataPath + "/save.json";
                Load();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void Save()
        {
            string json = JsonUtility.ToJson(CurrentData, true);
            File.WriteAllText(savePath, json);
        }

        public void Load()
        {
            if (File.Exists(savePath))
            {
                string json = File.ReadAllText(savePath);
                CurrentData = JsonUtility.FromJson<SaveData>(json);
            }
            else
            {
                CurrentData = new SaveData();
            }
        }
    }
}