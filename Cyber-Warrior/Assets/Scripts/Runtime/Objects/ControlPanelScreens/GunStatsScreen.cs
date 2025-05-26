using Data.UnityObjects;
using UnityEngine;

namespace Objects.ControlPanelScreens
{
    public class GunStatsScreen : PanelScreenBase
    {
        [SerializeField] private PlayerGunStatsSO playerGunStatsSo;
        
        private void OnEnable()
        {
        }
        private void OnDisable()
        {
        }
    }
}