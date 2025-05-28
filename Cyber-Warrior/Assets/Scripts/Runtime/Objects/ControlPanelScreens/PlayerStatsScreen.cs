using System;
using Data.UnityObjects;
using DG.Tweening;
using UnityEngine;

namespace Runtime.Objects.ControlPanelScreens
{
    public class PlayerStatsScreen : PanelScreenBase
    {
        [SerializeField] private PlayerStatsSO statsData;

        private void OnEnable()
        {
            ClosePanel();
        }

        public override void SetStatsToScreen()
        {
        }

        public override void OpenPanel()
        {
            transform.DOScale(Vector3.one, 0.1f);
        }

        public override void ClosePanel()
        {
            Debug.Log(transform.name);
            transform.DOScale(Vector3.zero, 0.1f);
        }
    }
}