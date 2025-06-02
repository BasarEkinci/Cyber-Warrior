using System;
using Runtime.Data.UnityObjects.Events;
using Runtime.Enums;
using UnityEngine;

namespace Runtime.Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameStateEvent gameStateEvent;

        private void OnEnable()
        {
            gameStateEvent.RaiseEvent(GameState.Base);
        }
    }
}