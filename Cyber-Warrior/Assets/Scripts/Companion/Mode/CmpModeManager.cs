namespace Companion.Mode
{
    public class CmpModeManager
    {
        public ICmpModeStrategy CurrentMode => _modes[_currentModeIndex];
        
        private readonly ICmpModeStrategy[] _modes;
        private int _currentModeIndex;
        
        public CmpModeManager()
        {
            _modes = new ICmpModeStrategy[]
            {
                new BaseMode(),
                new HealerMode(),
                new AttackerMode(),
            };
            _currentModeIndex = 0;
        }
        
        public void NextMode()
        {
            _currentModeIndex = (_currentModeIndex + 1) % _modes.Length;
        }
    }
}