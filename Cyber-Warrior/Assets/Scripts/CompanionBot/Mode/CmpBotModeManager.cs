using System.Collections.Generic;
using Data.UnityObjects.Events;
using Enums;
using Inputs;
using UnityEngine;

namespace CompanionBot.Mode
{
    public class CmpBotModeManager : MonoBehaviour
    {
        [SerializeField] private InputReader inputReader;
        [SerializeField] private GameStateEvent gameStateEvent;
        [SerializeField] private List<CmpBotMode> botModes;
        
        private CmpBotMode _currentMode;
        private GameState _currentGameState;

        private void Start()
        {
            _currentMode = botModes.Find(mode => mode is HealerBotMode);
        }

        private void OnEnable()
        {
            inputReader.OnSwitchMode += SwitchMode;
            gameStateEvent.OnEventRaised += OnGameStateChanged;
        }

        private void Update()
        {
            if (_currentMode == null)
            {
                return;
            }
            _currentMode.Execute();
        }
        private void SwitchMode()
        {
            if (_currentGameState != GameState.Action)
            {
                return;
            }

            switch (_currentMode)
            {
                case HealerBotMode:
                    _currentMode = botModes.Find(mode => mode is AttackerBotMode);
                    _currentMode?.Initialize();
                    break;
                case AttackerBotMode:
                    _currentMode = botModes.Find(mode => mode is HealerBotMode);
                    _currentMode?.Initialize();
                    break;
            }
        }

        private void OnDisable()
        {
            inputReader.OnSwitchMode -= SwitchMode;
            gameStateEvent.OnEventRaised -= OnGameStateChanged;
        }

        private void OnGameStateChanged(GameState state)
        {
            _currentGameState = state;
            switch (state)
            {
                case GameState.Base:
                    _currentMode = botModes.Find(mode => mode is BaseBotMode);
                    _currentMode?.Initialize();
                    break;
                case GameState.Action:
                    _currentMode = botModes.Find(mode => mode is HealerBotMode);
                    _currentMode?.Initialize();
                    break;
            }
        }
    }
}