using System;
using System.Collections.Generic;
using Runtime.Enums;
using UnityEngine;

namespace Runtime.Objects.ControlPanelScreens
{
    public class PanelManager : MonoBehaviour
    {
        public bool IsStatsPanelActive => gameStatsScreen.IsPanelActive;
        
        [SerializeField] private PanelScreenBase gunStatsScreen;
        [SerializeField] private PanelScreenBase playerStatsScreen;
        [SerializeField] private PanelScreenBase botStatsScreen;
        [SerializeField] private PanelScreenBase gameStatsScreen;
        
        private List<PanelScreenBase> _screens = new();
        private PanelScreenBase _currentActiveScreen;

        private void Awake()
        {
            _screens.Add(gunStatsScreen);
            _screens.Add(playerStatsScreen);
            _screens.Add(botStatsScreen);
            _screens.Add(gameStatsScreen);
            CloseAllPanels();
        }
        
        public void OpenPanel(ItemType type)
        {
            CloseAllPanels();

            _currentActiveScreen = type switch
            {
                ItemType.Player => playerStatsScreen,
                ItemType.Companion => botStatsScreen,
                ItemType.Gun => gunStatsScreen,
                _ => _currentActiveScreen
            };

            _currentActiveScreen?.OpenPanel();
        }

        public void CloseAllPanels()
        {
            foreach (var screen in _screens)
            {
                screen.ClosePanel();
            }
            _currentActiveScreen = null;
        }

        public void OpenGameStatsPanel()
        {
            CloseAllPanels();
            gameStatsScreen.OpenPanel();
            _currentActiveScreen = gameStatsScreen;
        }

        public void CloseGameStatsPanel()
        {
            gameStatsScreen.ClosePanel();
            _currentActiveScreen = null;
        }
    }
}