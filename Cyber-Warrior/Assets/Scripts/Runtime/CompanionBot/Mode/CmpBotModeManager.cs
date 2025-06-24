using System;
using System.Collections.Generic;
using System.Linq;
using Runtime.Data.UnityObjects.Events;
using Runtime.Enums;
using Runtime.Inputs;
using UnityEngine;


namespace Runtime.CompanionBot.Mode
{
    public class CmpBotModeManager : MonoBehaviour
    {
        public InputReader InputReader => _inputReader;
        
        private InputReader _inputReader;
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
            if (_inputReader == null)
            {
                _inputReader = FindFirstObjectByType<InputReader>();
            }
            _inputReader.OnSwitchMode += SwitchModeInActionState;
            gameStateEvent.OnEventRaised += OnGameStateChanged;
            ChangeModeTo(_botModes[0]);
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
            if (_currentMode == null)
            {
                return;
            }
            _currentMode.Move(transform,Time.fixedDeltaTime);
        }

        private void OnDisable()
        {
            _inputReader.OnSwitchMode -= SwitchModeInActionState;
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
        /// Handles game state changes between base and action states by updating the current game state,
        /// filtering active bot modes for the new state, and switching to the first available mode.
        /// </summary>
        /// <param name="state">New Game State</param>
        private void OnGameStateChanged(GameState state)
        {
            _currentGameState = state;
            _activeModes = _botModes
                .Where(m => m.ValidGameState == state)
                .ToList();
            
            if (_activeModes.Count > 0 || _currentGameState == GameState.Base)
            {
                ChangeModeTo(_activeModes[0]);
            }
        }

        private void ChangeModeTo(CmpBotMode newMode)
        {
            _currentMode?.ExitState();
            _currentMode = newMode;
            _currentMode?.Initialize();
            if (_currentMode != null) 
                botModeEvent.RaiseEvent(_currentMode.mode);
        }
    }
}