using DG.Tweening;
using Enums;
using UnityEngine;

namespace Objects.ControlPanelScreens
{
    public class PanelScreenBase : MonoBehaviour
    {
        public UpgradeItemType Type => type;
        [SerializeField] private UpgradeItemType type;

        private void OnEnable()
        {
            transform.DOScale(Vector3.zero, 0.1f).SetEase(Ease.Linear).From();
        }
    }
}