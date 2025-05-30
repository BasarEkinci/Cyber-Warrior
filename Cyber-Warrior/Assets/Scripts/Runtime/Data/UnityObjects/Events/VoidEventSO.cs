using UnityEngine;
using UnityEngine.Events;

namespace Runtime.Data.UnityObjects.Events
{
    [CreateAssetMenu(fileName = "VoidEvent", menuName = "Scriptable Objects/Events/VoidEvent")]
    public class VoidEventSO : ScriptableObject
    {
        public UnityAction OnEventRaised;

        public void Invoke()
        {
            OnEventRaised?.Invoke();
        }
    }
}

