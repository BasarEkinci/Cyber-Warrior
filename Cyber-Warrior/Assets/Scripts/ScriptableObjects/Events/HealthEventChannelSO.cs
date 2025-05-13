using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjects.Events
{
    [CreateAssetMenu(fileName = "HealthEvent", menuName = "Scriptable Objects/Events/HealthEvent")]
    public class HealthEventChannelSO : ScriptableObject
    {
        public UnityAction<float> OnHealthChanged;

        public void Invoke(float health)
        {
            OnHealthChanged?.Invoke(health);
        }
    }
}
