using UnityEngine;
using UnityEngine.Events;

namespace Runtime.Data.UnityObjects.Events
{
    [CreateAssetMenu(fileName = "Event Channel", menuName = "Scriptable Objects/Events", order = 0)]
    public class EventChannelSO<T> : ScriptableObject
    {
        public UnityAction<T> OnEventRaised;
        
        public void RaiseEvent(T item)
        {
            OnEventRaised?.Invoke(item);
        }
    }
}