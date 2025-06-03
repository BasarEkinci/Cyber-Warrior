using Runtime.Managers;
using UnityEngine;

namespace Runtime.Objects.ControlPanelScreens
{
    public abstract class PanelScreenBase : MonoBehaviour
    {
        public LevelManager levelManager;
        public abstract bool IsPanelActive { get; set; }
        public abstract void SetStatsToScreen();
        public abstract void OpenPanel();
        public abstract void ClosePanel();
    }
}