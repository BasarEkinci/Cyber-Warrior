using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Inputs;
using ScriptableObjects;
using ScriptableObjects.Events;
using UnityEngine;

namespace Combat.Components
{
    public class PlayerGun : MonoBehaviour
    {
        [SerializeField] private HoldInputChannelSO holdInputChannelSo;
        [SerializeField] private PlayerGunBaseStats playerGunBaseStats;
        [SerializeField] private Transform gunBarrelTransform;
        [SerializeField] private LineRenderer lineRenderer;
        
        private IPlayerInput _inputReader;
        private GameObject _crosshair;
        private CancellationTokenSource _cancellationTokenSource;
        private bool _isFiring;
        private void OnEnable()
        {
            _inputReader = new InputReader(holdInputChannelSo);
            _crosshair = GameObject.FindWithTag("Crosshair");
            holdInputChannelSo.OnFireStart += OnFireStart;
            holdInputChannelSo.OnFireEnd += OnFireEnd;
        }

        private void OnFireEnd()
        {
            _isFiring = false;
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
        }

        private void OnFireStart() 
        {
            _isFiring = true;
            _cancellationTokenSource = new CancellationTokenSource();
            FireGunAsync(_cancellationTokenSource.Token).Forget();
        }

        private void Update()
        {
            DrawLineToCrosshair();
        }
        
        private void OnDisable()
        {
            holdInputChannelSo.OnFireStart -= OnFireStart;
            holdInputChannelSo.OnFireEnd -= OnFireEnd;
            if (_inputReader is InputReader disposableInput)
            {
                disposableInput.Dispose();
            }
        }

        private async UniTaskVoid FireGunAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                Debug.Log("Fire");
                await UniTask.Delay(TimeSpan.FromSeconds(playerGunBaseStats.attackInterval),
                    cancellationToken: cancellationToken);
            }
        }
        
        private void DrawLineToCrosshair()
        {
            lineRenderer.SetPosition(0, gunBarrelTransform.position);
            lineRenderer.SetPosition(1, _crosshair.transform.position);
        }
    }
}