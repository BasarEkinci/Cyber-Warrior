using System;
using System.Collections.Generic;
using CompanionBot.Controller;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Inputs;
using Interfaces;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace Objects
{
    public class UpgradeManager : MonoBehaviour, IInteractable
    {
        [Header("Upgrade Area Objects")]
        [SerializeField] private List<GameObject> upgradeAreaObjects;
        
        [Header("Components")]
        [SerializeField] private Image screenBackgroundImage;
        [SerializeField] private CmpBotLevelDataSO cmpBotLevelData;
        [SerializeField] private ScrapData scrapData;
        [SerializeField] private InteractableData interactableData;
        [SerializeField] private InputReader inputReader;
        
        [Header("Screen Colors")]
        [SerializeField] private Color defaultColor;
        [SerializeField] private Color errorColor;
        [SerializeField] private Color successColor;
        
        private CmpManager _companionManager;
        private float _holdStartTime;
        private bool _canInteractable;
        private bool _interactionTriggered;
        private void Awake()
        {
            _companionManager = FindObjectOfType<CmpManager>();
        }

        private void OnEnable()
        {
            inputReader.OnInteractPressed += HandleInteractPressed;
            inputReader.OnInteractCanceled += HandleInteractCanceled;
            interactableData.onPress.AddListener(OnInteract);
            interactableData.onHold.AddListener(OnHold);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _canInteractable = true;
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _canInteractable = false;
            }
        }

        private void OnDisable()
        {
            inputReader.OnInteractPressed -= HandleInteractPressed;
            inputReader.OnInteractCanceled -= HandleInteractCanceled;
            interactableData.onPress.RemoveListener(OnInteract);
            interactableData.onHold.RemoveListener(OnHold);
        }

        private void HandleInteractCanceled()
        {

        }

        private void HandleInteractPressed()
        {
            _holdStartTime = Time.time;
            if (!_canInteractable) return;

            float heldTime = Time.time - _holdStartTime;

            if (heldTime >= interactableData.interactDuration)
                interactableData.onHold?.Invoke();
            else
                interactableData.onPress?.Invoke();
        }


        private void OnHold()
        {
            Debug.Log("Hold Interact with UpgradeManager");
            /*
            if (_companionManager.CmpBotLevel == cmpBotLevelData.MaxLevel)
            {
                Ignore();
                return;
            }
            if (!scrapData.TrySpendScarp(cmpBotLevelData.levelDataList[_companionManager.CmpBotLevel +  1].price))
            {
                Ignore();
                return;
            }
            Approve();
            _companionManager.Upgrade();*/
        }
        public void OnInteract()
        {
            Debug.Log("Interact with UpgradeManager");
            /*
            Approve();*/
        }
        private void Ignore()
        {
            screenBackgroundImage.DOColor(errorColor, 0.1f).SetLoops(2, LoopType.Yoyo);
        }

        private void Approve()
        {
            screenBackgroundImage.DOColor(successColor, 0.1f).SetLoops(2, LoopType.Yoyo);
            UpgradeActions();
        }

        private async void UpgradeActions()
        {
            for (int i = 0; i < upgradeAreaObjects.Count; i++)
            {
                IAnimated animated = upgradeAreaObjects[i].GetComponent<IAnimated>();
                animated.Animate();
                await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
            }
        }
    }
}
