using Data.UnityObjects;
using Inputs;
using UnityEngine;

namespace Managers
{
    public class CmpBotUpgradeManager : MonoBehaviour
    {
        [SerializeField] private InputReader inputReader;
        [SerializeField] private LevelManager levelManager;
        [SerializeField] private CmpBotDataSO cmpBotData;
        [SerializeField] private VoidEventSO onUpgradeEvent;
        [SerializeField] private float interactHoldTime = 2f;
        
        private bool _isInRange;
        private bool _isHoldingInteractButton;
        private float _currentHoldTime;
        
        private int _currentLevel;
        
        private void OnEnable()
        {
            inputReader.OnInteractPressed += OnInteractPressed;
            inputReader.OnInteractCanceled += OnInteractCanceled;
        }
        private void Update()
        {
            if (_isInRange && _isHoldingInteractButton)
            {
                _currentHoldTime += Time.deltaTime;
                if (_currentHoldTime >= interactHoldTime)
                {
                    if (ScarpAmountManager.Instance.TrySpendScarp(cmpBotData.statDataList[levelManager.CurrentLevel].levelPrice))
                    {
                        onUpgradeEvent.Invoke();
                        _currentHoldTime = 0f;
                    }
                    else
                    {
                        Debug.Log("Not enough scarp");
                    }
                }
            }
            else
            {
                _currentHoldTime = 0f;
            }
            
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _isInRange = true;
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _isInRange = false;
            }
        }

        private void OnDisable()
        {
            inputReader.OnInteractPressed -= OnInteractPressed;
            inputReader.OnInteractCanceled -= OnInteractCanceled;   
        }
        
        private void OnInteractCanceled()
        {
            _isHoldingInteractButton = false;
        }
        
        private void OnInteractPressed()
        {
            _isHoldingInteractButton = true;
        }
    }
}