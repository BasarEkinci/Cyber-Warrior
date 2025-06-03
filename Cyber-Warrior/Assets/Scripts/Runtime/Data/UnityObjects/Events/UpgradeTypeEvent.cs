using Runtime.Enums;
using UnityEngine;

namespace Runtime.Data.UnityObjects.Events
{
    [CreateAssetMenu(fileName = "Upgrade Item Type Event", menuName = "Scriptable Objects/Events/UpgradeItemType", order = 0)]
    public class UpgradeTypeEvent : EventChannelSO<ItemType>
    {
        
    }
}