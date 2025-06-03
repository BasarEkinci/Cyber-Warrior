using Runtime.Data.UnityObjects.Events;
using Runtime.Enums;
using UnityEngine;

namespace Runtime.Objects
{
    public class UpgradeArea : MonoBehaviour
    {
        public ItemType Type => type;
        [SerializeField] private ItemType type;
        [SerializeField] private Transform cmpWaitPoint;
        [SerializeField] private TransformEventChannel eventChannel;
        [SerializeField] private UpgradeTypeEvent upgradeTypeEvent;
        [SerializeField] private BooleanEventChannel booleanEventChannel;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                eventChannel.RaiseEvent(cmpWaitPoint);
                upgradeTypeEvent.RaiseEvent(type);
                booleanEventChannel.RaiseEvent(true);
            }    
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                eventChannel.RaiseEvent(null);
                upgradeTypeEvent.RaiseEvent(ItemType.None);
                booleanEventChannel.RaiseEvent(false);
            }    
        }
    }
}
