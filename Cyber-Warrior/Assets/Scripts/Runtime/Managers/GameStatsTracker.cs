using Runtime.Data.UnityObjects.Events;
using Runtime.Data.UnityObjects.ObjectData;
using Runtime.Enums;
using UnityEngine;

namespace Runtime.Managers
{
    public class GameStatsTracker : MonoBehaviour
    {
        [SerializeField] private StatChangeEvent statChangeEvent;
        [SerializeField] private GameStatsDataSO gameStats;

        private void OnEnable()
        {
            statChangeEvent.OnEventRaised += UpdateStats;
        }

        private void UpdateStats(StatType statType)
        {
            switch (statType)
            {
                case StatType.Deaths:
                    gameStats.UpdateDeaths();
                    break;
                case StatType.GamePlayed:
                    gameStats.UpdateGamePlayed();
                    break;
                case StatType.KilledEnemies:
                    gameStats.UpdateKills();
                    break;
            }
        }

    }
}