using System;
using System.Collections.Generic;
using System.Linq;
using Data.UnityObjects.Events;
using Enums;
using Inputs;
using UnityEngine;


namespace Runtime.CompanionBot.Mode
{
    public class CmpBotModeManager : MonoBehaviour
    {
        [SerializeField] private InputReader inputReader;
        [SerializeField] private GameStateEvent gameStateEvent;
        [SerializeField] private BotModeEvent botModeEvent;
        [SerializeField] private List<CmpBotMode> botModes;
        
        private CmpBotMode _currentMode;
        private GameState _currentGameState;
        
        private readonly Dictionary<GameState, Type> _stateModeMap = new Dictionary<GameState, System.Type>
        {
            { GameState.Base, typeof(BaseBotMode) },
            { GameState.Action, typeof(HealerBotMode) }
        };
        private void OnEnable()
        {
            inputReader.OnSwitchMode += SwitchModeInActionState;
            gameStateEvent.OnEventRaised += OnGameStateChanged;
        }

        private void Update()
        {
            if (_currentMode == null)
            {
                return;
            }
            _currentMode.Execute();
            _currentMode.RotateBehaviour(transform);
        }

        private void FixedUpdate()
        {
            _currentMode.MoveBehaviourFixed(transform);
        }

        private void OnDisable()
        {
            inputReader.OnSwitchMode -= SwitchModeInActionState;
            gameStateEvent.OnEventRaised -= OnGameStateChanged;
        }
        
        private void SwitchModeInActionState()
        {
            var currentValidModes = botModes.Where(mode => mode.ValidGameState == _currentGameState).ToList();
            int currentIndex = currentValidModes.IndexOf(_currentMode);
            int nextIndex = (currentIndex + 1) % currentValidModes.Count;
            ChangeModeTo(currentValidModes[nextIndex]);
        }
        
        /// <summary>
        /// This method is called only when the game state changes base to action or vice versa.
        /// </summary>
        /// <param name="state">New Game State</param>
        private void OnGameStateChanged(GameState state)
        {
            if (_stateModeMap.TryGetValue(state, out var modeType))
            {
                ChangeModeTo(botModes.Find(mode => mode.GetType() == modeType));
            }
        }

        private void ChangeModeTo(CmpBotMode newMode)
        {
            _currentMode = newMode;
            _currentMode?.Initialize();
            if (_currentMode != null) 
                botModeEvent.RaiseEvent(_currentMode.mode);
        }
    }
}