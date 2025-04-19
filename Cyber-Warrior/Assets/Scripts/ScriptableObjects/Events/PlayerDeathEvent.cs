using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjects.Events
{
    [CreateAssetMenu(fileName = "PlayerDeathEvent", menuName = "Scriptable Objects/Events/PlayerDeathEvent")]
    public class PlayerDeathEvent : ScriptableObject
    {
        public UnityAction OnPlayerDeath;

        public void Invoke()
        {
            OnPlayerDeath?.Invoke();
        }
    }
}

