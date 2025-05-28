using DG.Tweening;
using UnityEngine;

namespace Runtime.Objects.ControlPanelScreens
{
    public class GameStatsScreen : PanelScreenBase
    {
        public override void SetStatsToScreen()
        {
        }

        public override void OpenPanel()
        {
            transform.DOScale(Vector3.one, 0.1f);
        }

        public override void ClosePanel()
        {
            transform.DOScale(Vector3.zero, 0.1f);
        }
    }
}