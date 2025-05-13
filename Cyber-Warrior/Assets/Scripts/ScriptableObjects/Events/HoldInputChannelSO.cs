using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjects.Events
{
    [CreateAssetMenu(fileName = "Hold Input", menuName = "Scriptable Objects/Events/HoldInput", order = 0)]
    public class HoldInputChannelSO : ScriptableObject
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