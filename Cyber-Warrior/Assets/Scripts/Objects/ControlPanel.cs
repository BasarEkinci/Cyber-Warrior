using System.Collections.Generic;
using Data.UnityObjects;
using Enums;
using Inputs;
using Interfaces;
using UnityEngine;

namespace Objects
{
    public class ControlPanel : MonoBehaviour,IInteractable
    {
        [SerializeField] private UpgradeItemType type;

        [Header("Panels")] 
        [SerializeField] private List<GameObject> panelList;
        
        [Header("Control Panel Event Channel")]
        [SerializeField] private EventChannelSO<UpgradeItemType> upgradeItemEventSO;
        
        [Header("Class References")]
        [SerializeField] private InputReader inputReader;

        private bool _isPlayerInRange;
        private int _currentPanelIndex = 0;

        private void OnEnable()
        {
            upgradeItemEventSO.OnEventRaised += OnUpgradeItemChange;
            for (int i = 0; i < panelList.Count; i++)
            {
                panelList[i].SetActive(false);
            }
            inputReader.OnInteractPressed += OnInteract;
        }

        private void OnUpgradeItemChange(UpgradeItemType arg0)
        {
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                panelList[_currentPanelIndex].SetActive(true);
                _isPlayerInRange = true;
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _isPlayerInRange = false;
                panelList[_currentPanelIndex].SetActive(false);
                _currentPanelIndex = 0;
            }
        }
        
        private void OnDisable()
        {
            inputReader.OnInteractPressed -= OnInteract;
        }
        
        /// <summary>
        /// When the player is in range and presses the interact button, this method invokes the
        /// panel change event and changes the panel.
        /// </summary>
        public void OnInteract()
        {
            if (_isPlayerInRange)
            {
                if (_currentPanelIndex == panelList.Count - 1)
                {
                    _currentPanelIndex = 0;
                    return;
                }
                _currentPanelIndex++;
                ChangePanel(panelList[_currentPanelIndex - 1], panelList[_currentPanelIndex]);
            }
        }
        
        private void ChangePanel(GameObject previousPanel, GameObject newPanel)
        {
            previousPanel.SetActive(false);
            newPanel.SetActive(true);
        }
    }
}
