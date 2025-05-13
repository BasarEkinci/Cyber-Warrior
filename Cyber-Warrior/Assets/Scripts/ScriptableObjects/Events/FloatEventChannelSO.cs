using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjects.Events
{
    [CreateAssetMenu(fileName = "FloatEvent", menuName = "Scriptable Objects/Events/FloatEvent")]
    public class FloatEventChannelSO : ScriptableObject
    {
        public UnityAction<float> OnEventRaise;

        public void Invoke(float health)
        {
            OnEventRaise?.Invoke(health);
        }
    }
}
