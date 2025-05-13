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
        [SerializeField] private Transform gunBarrelTransform;
        
        [SerializeField] private HoldInputChannelSO holdInputChannelSo;
        [SerializeField] private PlayerGunBaseStats playerGunBaseStats;
        
        [SerializeField] private LineRenderer lineRenderer;
        [SerializeField] private ParticleSystem muzzleFlash;
        
        private IPlayerInput _inputReader;
        private GameObject _crosshair;
        private CancellationTokenSource _cancellationTokenSource;
        
        private void OnEnable()
        {
            _inputReader = new InputReader(holdInputChannelSo);
            _crosshair = GameObject.FindWithTag("Crosshair");
            holdInputChannelSo.OnFireStart += OnFireStart;
            holdInputChannelSo.OnFireEnd += OnFireEnd;
            if(muzzleFlash.isPlaying) muzzleFlash.Stop();
        }

        private void OnFireEnd()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
        }

        private void OnFireStart() 
        {
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
                if (!muzzleFlash.isPlaying)
                {
                    muzzleFlash.Play();
                }
                //ADD VFX
                //ADD SFX
                await UniTask.Delay(TimeSpan.FromSeconds(playerGunBaseStats.attackInterval),
                    cancellationToken: cancellationToken);
                if (muzzleFlash.isPlaying)
                {
                    muzzleFlash.Stop();
                }
            }
        }
        
        private void DrawLineToCrosshair()
        {
            lineRenderer.SetPosition(0, gunBarrelTransform.position);
            lineRenderer.SetPosition(1, _crosshair.transform.position);
        }
    }
}