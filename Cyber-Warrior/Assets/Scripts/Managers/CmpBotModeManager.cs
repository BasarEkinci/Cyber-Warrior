using CompanionBot.Mode;

namespace Managers
{
    public class CmpBotModeManager
    {
        public ICmpBotModeStrategy CurrentBotMode => _modes[_currentModeIndex];
        
        private readonly ICmpBotModeStrategy[] _modes;
        private int _currentModeIndex;
        
        public CmpBotModeManager()
        {
            _modes = new ICmpBotModeStrategy[]
            {
                new BaseBotMode(),
                new HealerBotMode(),
                new AttackerBotMode(),
            };
            _currentModeIndex = 0;
        }
        
        public void NextMode()
        {
            _currentModeIndex = (_currentModeIndex + 1) % _modes.Length;
        }
    }
}