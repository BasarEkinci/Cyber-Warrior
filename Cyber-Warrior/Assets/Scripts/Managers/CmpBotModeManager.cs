using CompanionBot.Mode;
using Enums;
using UnityEngine;

namespace Managers
{
    public class CmpBotModeManager
    {
        public ICmpBotModeStrategy CurrentBotMode {get; private set;}
        
        private readonly ICmpBotModeStrategy[] _modes;
        private int _currentModeIndex;
        private bool _isLocked;

        public CmpBotModeManager()
        {
            _modes = new ICmpBotModeStrategy[]
            {
                new HealerBotMode(),
                new AttackerBotMode(),
            };
            _currentModeIndex = 0;
            CurrentBotMode = _modes[_currentModeIndex];
        }
        
        public void NextMode()
        {
            if (_isLocked)
            {
                Debug.Log("Mode changing locked");
                return;
            }
            Debug.Log("Mode changing not locked");
            if (_currentModeIndex == 0)
            {
                _currentModeIndex = 1;
            }
            else
            {
                _currentModeIndex = 0;
            }
            CurrentBotMode = _modes[_currentModeIndex];
        }

        public void SetMode(ICmpBotModeStrategy mode,GameState state)
        {
            if (state == GameState.Base)
            {
                _isLocked = true;
                CurrentBotMode = mode;
            }
            else
            {
                _isLocked = false;
                CurrentBotMode = mode;
            }
        }
    }
}