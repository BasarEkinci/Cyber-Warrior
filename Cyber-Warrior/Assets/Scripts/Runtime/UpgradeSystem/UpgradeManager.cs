using Runtime.Data.UnityObjects.Events;
using Runtime.Enums;
using Runtime.Inputs;
using UnityEngine;

namespace Runtime.UpgradeSystem
{
    public class UpgradeManager : MonoBehaviour
    {
        [SerializeField] private float upgradeTime = 2f;
        [SerializeField] private UpgradeTypeEvent upgradeTypeEvent;
        [SerializeField] private BooleanEventChannel booleanEventChannel;
        [SerializeField] private InputReader inputReader;
        
        private ItemType _currentUpgradeType = ItemType.None;
        private float _pressedTime = 0f;
        private float _releasedTime = 0f;
        private bool _canUpgrade = false;
        
        private void OnEnable()
        {
            upgradeTypeEvent.OnEventRaised += HandleUpgradeTypeChanged;
            booleanEventChannel.OnEventRaised += HandleBooleanEvent;
            inputReader.OnInteractPressed += HandleUpgradeButtonPressed;
            inputReader.OnInteractCanceled += HandleUpgradeButtonReleased;
        }

        private void HandleUpgradeButtonReleased()
        {
            if (_currentUpgradeType == ItemType.None || !_canUpgrade)
                return;
            _releasedTime = Time.deltaTime;
            
            if (_releasedTime - _pressedTime >= upgradeTime)
            {
                Debug.Log($"Upgrading {_currentUpgradeType} for {_releasedTime - _pressedTime} seconds.");
                _pressedTime = 0f;
            }
            else
            {
                Debug.Log("Upgrade Cancelled");
            }
        }

        private void HandleUpgradeButtonPressed()
        {
            _pressedTime = Time.deltaTime;
        }

        private void OnDisable()
        {
            upgradeTypeEvent.OnEventRaised -= HandleUpgradeTypeChanged;
            booleanEventChannel.OnEventRaised -= HandleBooleanEvent;
        }

        private void HandleBooleanEvent(bool isInArea)
        {
            _canUpgrade = isInArea;
            if (isInArea)
            {
                Debug.Log("Player is in upgrade area.");
            }
            else
            {
                Debug.Log("Player left upgrade area.");
                _currentUpgradeType = ItemType.None; // Reset upgrade type when leaving area
            }
        }

        private void HandleUpgradeTypeChanged(ItemType type)
        {
            _currentUpgradeType = 
                type == ItemType.None ? ItemType.None : type;
            
            Debug.Log(_currentUpgradeType);
        }
    }
}
