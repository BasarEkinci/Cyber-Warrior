using System;
using Runtime.Data.UnityObjects.Events;
using Runtime.Enums;
using UnityEngine;

namespace Runtime.Objects.Door
{
    public class Area : MonoBehaviour
    {
        [SerializeField] private GameStateEvent gameStateEvent;
        [SerializeField] private VoidEventSO levelSuccessEvent;
        [SerializeField] private GameState gameState;

        private GameState _currentGameState;
        
        private void OnEnable()
        {   
            gameStateEvent.OnEventRaised += OnGameStateChanged; 
        }

        private void OnGameStateChanged(GameState state)
        {
            _currentGameState = state;
        }

        private void OnDisable()
        {
            gameStateEvent.OnEventRaised -= OnGameStateChanged; 
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (_currentGameState == GameState.Action)
                {
                    Debug.Log("Level completed!");
                    levelSuccessEvent.Invoke();
                }
                gameStateEvent.RaiseEvent(gameState);
            }
        }
    }
}