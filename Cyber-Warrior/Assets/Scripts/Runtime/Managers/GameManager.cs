using System;
using Runtime.Data.UnityObjects.Events;
using Runtime.Enums;
using UnityEngine;

namespace Runtime.Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameObject gameOverPanel;
        [SerializeField] private GameStateEvent gameStateEvent;

        private void Start()
        {
            gameOverPanel.SetActive(false);
        }

        private void OnEnable()
        {
            gameStateEvent.OnEventRaised += HandleGameStateChange;
        }
        private void OnDisable()
        {
            gameStateEvent.OnEventRaised -= HandleGameStateChange;
        }
        private void HandleGameStateChange(GameState state)
        {
            if (state == GameState.GameOver)
            {
                gameOverPanel.SetActive(true);
            }
            else
            {
                gameOverPanel.SetActive(false);
            }
        }
    }
}