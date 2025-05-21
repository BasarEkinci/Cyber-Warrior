using System.Collections.Generic;
using Data.UnityObjects;
using Inputs;
using Interfaces;
using Objects.ControlPanelScreens;
using ScriptableObjects;
using UnityEngine;

namespace Objects
{
    public class ControlPanel : MonoBehaviour,IInteractable
    {
        [Header("Panels")] 
        [SerializeField] private List<GameObject> panelList;
        
        [Header("Control Panel Event Channel")]
        [SerializeField] private ControlPanelEvetSO controlPanelEventSO;
        
        [Header("Class References")]
        [SerializeField] private InputReader inputReader;

        private bool _isPlayerInRange;
        private int _currentPanelIndex = 0;

        private void OnEnable()
        {
            for (int i = 0; i < panelList.Count; i++)
            {
                panelList[i].SetActive(false);
            }
            inputReader.OnInteractPressed += OnInteract;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                panelList[_currentPanelIndex].SetActive(true);
                _isPlayerInRange = true;
                controlPanelEventSO.IsPlayerInRangeInvoke(_isPlayerInRange);
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _isPlayerInRange = false;
                panelList[_currentPanelIndex].SetActive(false);
                controlPanelEventSO.IsPlayerInRangeInvoke(_isPlayerInRange);
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
                    PanelScreenBase panel = ChangePanel(panelList[^1], 
                        panelList[_currentPanelIndex]).GetComponent<PanelScreenBase>();
                    controlPanelEventSO.OnPanelChangeInvoke(panel.Type);
                    return;
                }
                _currentPanelIndex++;
                ChangePanel(panelList[_currentPanelIndex - 1], panelList[_currentPanelIndex]);
            }
        }
        
        private GameObject ChangePanel(GameObject previousPanel, GameObject newPanel)
        {
            previousPanel.SetActive(false);
            newPanel.SetActive(true);
            return newPanel;
        }
    }
}
