using Runtime.Data.UnityObjects.Events;
using Runtime.Enums;
using Runtime.Inputs;
using UnityEngine;

namespace Runtime.Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameObject gameOverPanel;
        [SerializeField] private GameObject pausePanel;
        [SerializeField] private GameObject endGamePanel;
        [SerializeField] private VoidEventSO levelSuccessEvent;
        [SerializeField] private GameStateEvent gameStateEvent;
        [SerializeField] private InputReader inputReader;
        
        private bool _isPaused;
        private GameState _currentGameState;
        private GameState _previousGameState;
        private void Start()
        {
            endGamePanel.SetActive(false);
            gameOverPanel.SetActive(false);
        }

        private void OnEnable()
        {
            gameStateEvent.OnEventRaised += HandleGameStateChange;
            levelSuccessEvent.OnEventRaised += () =>
            {
                endGamePanel.SetActive(true);
            };
            inputReader.OnPauseGame += TogglePauseGame;
            gameOverPanel.SetActive(false);
            pausePanel.SetActive(false);
            gameStateEvent.RaiseEvent(GameState.Base);
        }

        private void TogglePauseGame()
        {
            _isPaused = !_isPaused;
            if (_isPaused && _currentGameState != GameState.GameOver)
            {
                gameStateEvent.RaiseEvent(GameState.Pause);
                pausePanel.SetActive(true);
            }
            else
            {
                gameStateEvent.RaiseEvent(_previousGameState);
                pausePanel.SetActive(false);
            }
        }

        private void OnDisable()
        {
            gameStateEvent.OnEventRaised -= HandleGameStateChange;
            levelSuccessEvent.OnEventRaised -= () =>
            {
                endGamePanel.SetActive(true);
            };
            inputReader.OnPauseGame -= TogglePauseGame;
        }
        private void HandleGameStateChange(GameState state)
        {
            _currentGameState = state;
            if (state != GameState.Pause)
            {
                _previousGameState = state;
            }
            if (state == GameState.GameOver)
            {
                gameOverPanel.SetActive(true);
                _previousGameState = state;
                return;
            }
            if (state == GameState.Pause)
            {
                pausePanel.SetActive(true);
            }
        }
    }
}