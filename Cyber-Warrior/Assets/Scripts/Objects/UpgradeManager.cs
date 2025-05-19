using System;
using System.Collections.Generic;
using CompanionBot.Controller;
using Cysharp.Threading.Tasks;
using DG.Tweening;
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
        
        [Header("Screen Colors")]
        [SerializeField] private Color defaultColor;
        [SerializeField] private Color errorColor;
        [SerializeField] private Color successColor;
        
        private CmpManager _companionManager;

        private void Awake()
        {
            _companionManager = FindObjectOfType<CmpManager>();
        }

        public void Interact()
        {
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
            _companionManager.Upgrade();
            Approve();
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
