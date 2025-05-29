using Runtime.Data.ValueObjects;
using UnityEngine;

namespace Runtime.Data.UnityObjects.ObjectData
{
    [CreateAssetMenu(fileName = "GameStats", menuName = "Scriptable Objects/GameStats")]
    public class GameStatsDataSO : ScriptableObject
    {
        public GameStatsData gameStats;

        public void UpdateKills() => gameStats.totalKilledEnemies++;
        public void UpdateDeaths() => gameStats.totalDeaths++;
        public void UpdateGamePlayed() => gameStats.totalGameTime++;
        
        public void ResetStats()
        {
            gameStats.totalKilledEnemies = 0;
            gameStats.totalDeaths = 0;
            gameStats.totalGameTime = 0;
        }
    }
}