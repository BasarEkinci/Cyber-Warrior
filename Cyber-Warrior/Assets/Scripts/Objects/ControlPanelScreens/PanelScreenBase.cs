using DG.Tweening;
using Enums;
using UnityEngine;

namespace Objects.ControlPanelScreens
{
    public class PanelScreenBase : MonoBehaviour
    {
        public ControlPanelScreenType Type => type;
        [SerializeField] private ControlPanelScreenType type;

        private void OnEnable()
        {
            transform.DOScale(Vector3.zero, 0.1f).SetEase(Ease.Linear).From();
        }
    }
}