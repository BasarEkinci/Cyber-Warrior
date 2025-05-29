using Data.UnityObjects.Events;
using Runtime.Enums;
using UnityEngine;

namespace Runtime.Data.UnityObjects.Events
{
    [CreateAssetMenu(fileName = "GameStateEvent", menuName = "Scriptable Objects/Events/GameStateEvent")]
    public class GameStateEvent : EventChannelSO<GameState>
    {
        
    }
}