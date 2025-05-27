using Data.UnityObjects;
using Inputs;
using Interfaces;
using Runtime.Interfaces;
using UnityEngine;

namespace Managers
{
    public class UpgradeManager : MonoBehaviour
    {
        [SerializeField] private InputReader inputReader;
        [SerializeField] private MonoBehaviour upgradeTarget;
        [SerializeField] private float holdTime = 2f;

        private IUpgradeable _upgradeable;
        private bool _isInRange;
        private bool _isHolding;
        private float _holdTimer;

        private void Awake()
        {
            _upgradeable = upgradeTarget as IUpgradeable;
            if (_upgradeable == null)
            {
                Debug.LogError("upgradeTarget must implement IUpgradeable!");
                Debug.Log(transform.name + " does not implement IUpgradeable.");
            }
        }

        private void OnEnable()
        {
            inputReader.OnInteractPressed += OnInteractPressed;
            inputReader.OnInteractCanceled += OnInteractCanceled;
        }

        private void OnDisable()
        {
            inputReader.OnInteractPressed -= OnInteractPressed;
            inputReader.OnInteractCanceled -= OnInteractCanceled;
        }

        private void Update()
        {
            if (_isInRange && _isHolding)
            {
                _holdTimer += Time.deltaTime;
                if (_holdTimer >= holdTime)
                {
                    TryUpgrade();
                    _holdTimer = 0f;
                }
            }
            else
            {
                _holdTimer = 0f;
            }
        }

        private void OnInteractPressed()
        {
            _isHolding = true;
        }

        private void OnInteractCanceled()
        {
            _isHolding = false;
        }

        private void TryUpgrade()
        {
            if (_upgradeable.CurrentLevel >= _upgradeable.MaxLevel)
            {
                Debug.Log("Max level reached.");
                return;
            }

            var price = _upgradeable.GetLevelPrice(_upgradeable.CurrentLevel);
            if (ScarpAmountManager.Instance.TrySpendScarp(price))
                _upgradeable.Upgrade();
            else
                Debug.Log("Not enough scrap.");
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
                _isInRange = true;
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
                _isInRange = false;
        }
    }
}