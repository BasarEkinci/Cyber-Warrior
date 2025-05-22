using Enums;
using UnityEngine;

namespace Data.UnityObjects.Events
{
    [CreateAssetMenu(fileName = "GameStateEvent", menuName = "Scriptable Objects/Events/GameStateEvent")]
    public class GameStateEvent : EventChannelSO<GameState>
    {
        
    }
}