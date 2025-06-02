using DG.Tweening;
using Runtime.Data.UnityObjects.ObjectData;
using Runtime.Managers;
using TMPro;
using UnityEngine;

namespace Runtime.Objects.ControlPanelScreens
{
    public class GameStatsScreen : PanelScreenBase
    {
        public override bool IsPanelActive { get; set; }
        
        [Header("Level Info")] 
        [SerializeField] private LevelManager cmpLevelManager;
        [SerializeField] private LevelManager playerLevelManager;
        
        [Header("Data")] 
        [SerializeField] private GameStatsDataSO data;

        [Header("Text")] 
        [SerializeField] private TMP_Text totalKills;
        [SerializeField] private TMP_Text totalDeath;
        [SerializeField] private TMP_Text runTime;
        [SerializeField] private TMP_Text botLevel;
        [SerializeField] private TMP_Text gunLevel;
        [SerializeField] private TMP_Text playerLevel;

        private void OnEnable()
        {
            SetStatsToScreen();
        }

        public override void SetStatsToScreen()
        {
            totalKills.text = $"Total Kills: {data.gameStats.totalKilledEnemies}";
            totalDeath.text = $"Total Deaths: {data.gameStats.totalDeaths}";
            runTime.text = $"Games Played: {data.gameStats.totalGameTime}";
            botLevel.text = $"Bot Level: {cmpLevelManager.CurrentLevel}";
            playerLevel.text = $"Player Level: {playerLevelManager.CurrentLevel + 1}";
            gunLevel.text = $"Gun Level: {levelManager.CurrentLevel + 1}";
        }

        public override void OpenPanel()
        {
            IsPanelActive = true;
            transform.DOScale(Vector3.one, 0.1f);
        }

        public override void ClosePanel()
        {
            IsPanelActive = false;
            transform.DOScale(Vector3.zero, 0.1f);
        }
    }
}