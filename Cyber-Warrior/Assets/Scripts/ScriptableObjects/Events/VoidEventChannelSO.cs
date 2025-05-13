using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjects.Events
{
    [CreateAssetMenu(fileName = "Void Event Channel", menuName = "Scriptable Objects/Events/Void Event", order = 0)]
    public class VoidEventChannelSO : ScriptableObject
    {
        public readonly UnityAction OnEventRaised = delegate { };
        
        public void RaiseEvent()
        {
            OnEventRaised?.Invoke();
        }
    }
}