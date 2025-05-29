using Runtime.Data.UnityObjects.Events;
using Runtime.Enums;
using UnityEngine;

namespace Runtime.Objects
{
    public class UpgradeArea : MonoBehaviour
    {
        public UpgradeItemType ItemType => type;

        [SerializeField] private UpgradeItemType type;
        [SerializeField] private Transform cmpWaitPoint;
        [SerializeField] private TransformEventChannel eventChannel;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                eventChannel.RaiseEvent(cmpWaitPoint);
            }    
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                eventChannel.RaiseEvent(null);
            }    
        }
    }
}
