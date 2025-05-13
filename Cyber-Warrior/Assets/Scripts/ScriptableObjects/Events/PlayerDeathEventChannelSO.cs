using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjects.Events
{
    [CreateAssetMenu(fileName = "PlayerDeathEvent", menuName = "Scriptable Objects/Events/PlayerDeathEvent")]
    public class PlayerDeathEventChannelSO : ScriptableObject
    {
        public UnityAction OnPlayerDeath;

        public void Invoke()
        {
            OnPlayerDeath?.Invoke();
        }
    }
}

