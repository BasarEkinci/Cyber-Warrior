using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "HealthEvent", menuName = "Scriptable Objects/Events/HealthEvent")]
    public class HealthEvent : ScriptableObject
    {
        private UnityAction<float> _onHealthChanged;
        
        public void Invoke(float health)
        {
            _onHealthChanged?.Invoke(health);
        }
    }
}
