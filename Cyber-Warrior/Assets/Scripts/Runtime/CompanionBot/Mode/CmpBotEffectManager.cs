using System.Collections.Generic;
using DG.Tweening;
using Runtime.Data.UnityObjects.Events;
using Runtime.Enums;
using Runtime.Inputs;
using Runtime.Objects;
using UnityEngine;

namespace Runtime.CompanionBot.Mode
{
    public class CmpBotEffectManager : MonoBehaviour
    {
        [SerializeField] private GameObject eyesLights;
        [SerializeField] private List<GameObject> eyesLines;
        [SerializeField] private List<ParticleSystem> muzzleFlashSystems;
        [SerializeField] private TransformEventChannel transformEventChannel;
        [SerializeField] private GameStateEvent gameStateEvent;
        
        private InputReader _inputReader;
        private Transform _current;
        private bool _canOpenEyes;
        private bool _isStatsOpen;
        private void OnEnable()
        {
            _inputReader = FindObjectOfType<InputReader>();
            _inputReader.OnStatsButtonPressed += OnStatsButtonPressed;
            gameStateEvent.OnEventRaised += OnGameStateChanged;
            transformEventChannel.OnEventRaised += OnTransformEventRaised;
            CloseEyesLights();
        }

        private void OnGameStateChanged(GameState state)
        {
            _canOpenEyes = state == GameState.Base;
            if (!_canOpenEyes)
            {
                CloseEyesLights();
            }
        }

        private void OnTransformEventRaised(Transform trs)
        {
            _current = trs;
            _canOpenEyes = _current == null;
            if (_current == null)
            {
                CloseEyesLights();
                return;
            }
            if (trs.gameObject.TryGetComponent(out UpgradeArea upgradeArea))
            {
                if (upgradeArea != null)
                {
                    OpenEyesLights();
                }
            }
        }

        private void OnDisable()
        {
            if (_inputReader != null)
            {
                _inputReader.OnStatsButtonPressed -= OnStatsButtonPressed;
            }
        }

        private void OnStatsButtonPressed()
        {
            _isStatsOpen = !_isStatsOpen;
            if (_isStatsOpen)
            {
                CloseEyesLights();
            }
            else
            {
                OpenEyesLights();
            }
        }

        public void PlayFireEffect()
        {
            foreach (var particle in muzzleFlashSystems)
            {
                particle?.Play();
            }
        }
        private void OpenEyesLights()
        {
            if (!_canOpenEyes) return;
            eyesLights.SetActive(true);
            foreach (var effect in eyesLines)
            {
                effect.transform.DOScaleZ(0.01f, 0.3f);
            }
        }

        private void CloseEyesLights()
        {
            eyesLights.SetActive(false);
            foreach (var effect in eyesLines)
            {
                effect.transform.DOScaleZ(0f, 0.3f);
            }
        }
     }
}