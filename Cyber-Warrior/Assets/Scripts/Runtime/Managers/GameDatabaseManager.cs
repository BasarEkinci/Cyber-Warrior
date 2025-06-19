using Runtime.Extensions;
using UnityEngine;

namespace Runtime.Managers
{
    public static class SaveKeys
    {
        public const string PlayerLevel = "PlayerLevel";
        public const string ScrapAmount = "ScrapAmount";
        public const string GunLevel = "GunLevel";
        public const string CompanionLevel = "CompanionLevel";
        public const string TotalKills = "TotalKills";
        public const string TotalDeaths = "TotalDeaths";
    }
    public class GameDatabaseManager : MonoSingleton<GameDatabaseManager>
    {
        public void SaveData<T>(string key, T value)
        {
            switch (value)
            {
                case int i:
                    PlayerPrefs.SetInt(key, i);
                    break;
                case float f:
                    PlayerPrefs.SetFloat(key, f);
                    break;
                case string s:
                    PlayerPrefs.SetString(key, s);
                    break;
                default:
                    Debug.LogError($"Unsupported data type: {typeof(T)}");
                    break;
            }
            PlayerPrefs.Save();
        }

        public int LoadData(string key)
        {
            return PlayerPrefs.GetInt(key);
        }
        
        public void ClearAllData()
        {
            PlayerPrefs.DeleteAll();
        }
        
        public void SaveAllData()
        {
            PlayerPrefs.Save();
        }
    }
}