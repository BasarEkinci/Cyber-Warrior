using UnityEngine;
using UnityEngine.Events;

namespace Data.UnityObjects
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

