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
        
        private List<CmpBotMode> _botModes = new ();
        private List<CmpBotMode> _activeModes = new();
        private Dictionary<GameState, Type> _stateModeMap = new();
        
        private CmpBotMode _currentMode;
        private GameState _currentGameState;

        private void Awake()
        {
            _botModes.AddRange(GetComponentsInChildren<CmpBotMode>(true));
            _stateModeMap.Add(GameState.Action, typeof(HealerBotMode));
            _stateModeMap.Add(GameState.Base, typeof(BaseBotMode));
        }

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
            if (_currentGameState != GameState.Action || _activeModes.Count == 0) return;

            int currentIndex = _activeModes.IndexOf(_currentMode);
            int nextIndex = (currentIndex + 1) % _activeModes.Count;
            ChangeModeTo(_activeModes[nextIndex]);
        }
        
        /// <summary>
        /// This method is called only when the game state changes base to action or vice versa.
        /// </summary>
        /// <param name="state">New Game State</param>
        private void OnGameStateChanged(GameState state)
        {
            _currentGameState = state;
            _activeModes = _botModes
                .Where(m => m.ValidGameState == state)
                .ToList();
            
            if (_activeModes.Count > 0)
            {
                ChangeModeTo(_activeModes[0]);
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