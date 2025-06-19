using Runtime.Data.UnityObjects.Events;
using Runtime.Enums;
using Runtime.Managers;
using UnityEngine;
namespace Runtime.Data.UnityObjects.ObjectData
{
    public class GameStatsDataTracker : MonoBehaviour
    {
        public int TotalKillsInCurrentRun { get; private set; }
        public int TotalCollectedScrapInCurrentRun { get; private set; }
        
        [SerializeField] private VoidEventSO onEnemyKilled;
        [SerializeField] private VoidEventSO onPlayerDeath;
        [SerializeField] private IntegerEventSo onScrapCollected;
        [SerializeField] private GameStateEvent onGameStateChanged;
        
        private int _totalKilledEnemies;
        private int _totalDeaths;

        private void OnEnable()
        {
            _totalKilledEnemies = GameDatabaseManager.Instance.LoadData(SaveKeys.TotalKills);
            _totalDeaths = GameDatabaseManager.Instance.LoadData(SaveKeys.TotalDeaths);
            onEnemyKilled.OnEventRaised += IncrementTotalKilledEnemies;
            onPlayerDeath.OnEventRaised += IncrementTotalDeaths;
            onScrapCollected.OnEventRaised += IncrementTotalCollectedScrap;
            onGameStateChanged.OnEventRaised += ResetStatsOnNewRun;
        }
        private void OnDisable()
        {
            onEnemyKilled.OnEventRaised -= IncrementTotalKilledEnemies;
            onPlayerDeath.OnEventRaised -= IncrementTotalDeaths;
            onScrapCollected.OnEventRaised -= IncrementTotalCollectedScrap;
            onGameStateChanged.OnEventRaised -= ResetStatsOnNewRun;
        }

        private void ResetStatsOnNewRun(GameState state)
        {
            if (state == GameState.Base)
            {
                TotalKillsInCurrentRun = 0;
                TotalCollectedScrapInCurrentRun = 0;
            }
        }

        private void IncrementTotalCollectedScrap(int value)
        {
            TotalCollectedScrapInCurrentRun += value;
        }

        private void IncrementTotalDeaths()
        {
            _totalDeaths++;
            GameDatabaseManager.Instance.SaveData(SaveKeys.TotalDeaths, _totalDeaths);
        }

        private void IncrementTotalKilledEnemies()
        {
            _totalKilledEnemies++;
            TotalKillsInCurrentRun++;
            GameDatabaseManager.Instance.SaveData(SaveKeys.TotalKills, _totalKilledEnemies);
        }
    }
}