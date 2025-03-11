using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "HealthEvent", menuName = "Scriptable Objects/Events/HealthEvent")]
    public class HealthEvent : ScriptableObject
    {
        public UnityAction<float> OnHealthChanged;

        public void Invoke(float health)
        {
            OnHealthChanged?.Invoke(health);
        }
    }
}
