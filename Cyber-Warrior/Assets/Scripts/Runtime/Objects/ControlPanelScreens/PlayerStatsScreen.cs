using Data.UnityObjects;
using DG.Tweening;
using Runtime.Data.UnityObjects.ObjectData;
using Runtime.Data.ValueObjects;
using TMPro;
using UnityEngine;

namespace Runtime.Objects.ControlPanelScreens
{
    public class PlayerStatsScreen : PanelScreenBase
    {
        public override bool IsPanelActive { get; set; }
        
        [Header("Data")]
        [SerializeField] private PlayerStatsSO statsData;

        [Header("Text")] 
        [SerializeField] private TMP_Text maxHealth;
        [SerializeField] private TMP_Text level;

        private PlayerStatsData _data;

        private void Start()
        {
            ClosePanel();
        }

        private void OnEnable()
        {
            SetStatsToScreen();
        }


        public override void SetStatsToScreen()
        {
            _data = statsData.playerStatsDataList[levelManager.CurrentLevel];
            maxHealth.text = "Max Health: " + _data.maxHealth;
            level.text = "Level: " + levelManager.CurrentLevel;
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