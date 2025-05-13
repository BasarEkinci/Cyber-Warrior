using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjects.Events
{
    [CreateAssetMenu(fileName = "Gun Fire Event", menuName = "Scriptable Objects/Events/Gun Fire Event", order = 0)]
    public class GunFireEventSO : ScriptableObject
    {
        public UnityAction OnFireStart = delegate { };
        public UnityAction OnFireEnd = delegate { };
        
        public void RaiseFireStart()
        {
            OnFireStart?.Invoke();
        }
        
        public void RaiseFireEnd()
        {
            OnFireEnd?.Invoke();
        }
    }
}