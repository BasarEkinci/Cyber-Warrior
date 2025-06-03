using Runtime.Data.UnityObjects.Events;
using Runtime.Data.UnityObjects.ObjectData;
using Runtime.Inputs;
using Runtime.Managers;
using UnityEngine;

namespace Runtime.UpgradeSystem
{
    public class CmpBotUpgradeManager : MonoBehaviour
    {
        [Header("Scriptables")]
        [SerializeField] private VoidEventSO onUpgradeEvent;
        [SerializeField] private CmpBotDataSO cmpBotData;
        
        [Header("Class References")]
        public LevelManager levelManager;
        public InputReader inputReader;

        [Header("Settings")]
        public float interactHoldTime = 2f;
        
        private void Awake()
        {
            inputReader = FindFirstObjectByType<InputReader>();
        }
        
        
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
            UpdateHandler();   
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

        private void UpdateHandler()
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
    }
}