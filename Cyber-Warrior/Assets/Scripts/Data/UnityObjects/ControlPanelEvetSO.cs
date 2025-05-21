using System;
using Enums;
using UnityEngine;

namespace Data.UnityObjects
{
    [CreateAssetMenu(fileName = "Control Panel Event Channel", menuName = "Scriptable Objects/Events/Objects", order = 0)]
    public class ControlPanelEvetSO : ScriptableObject
    {
        public Action<ControlPanelScreenType> OnPanelChange;
        public Action<bool> IsPlayerInRange;

        public void IsPlayerInRangeInvoke(bool value)
        {
            IsPlayerInRange?.Invoke(value);
        }
        
        public void OnPanelChangeInvoke(ControlPanelScreenType type)
        {
            OnPanelChange?.Invoke(type);
        }
    }
}