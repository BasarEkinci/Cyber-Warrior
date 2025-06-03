using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Data.UnityObjects;
using Runtime.Data.UnityObjects.Events;
using Runtime.Data.UnityObjects.ObjectData;
using Runtime.Data.ValueObjects;
using Runtime.Inputs;
using Runtime.Interfaces;
using Runtime.Managers;
using UnityEngine;

namespace Runtime.Combat.Components
{
    public class PlayerGun : MonoBehaviour
    {   
        [Header("Data")]
        [SerializeField] private PlayerGunStatsSO playerGunStatsSo;
        [SerializeField] private VoidEventSO playerDeathEvent;
        
        [Header("Visuals")]
        [SerializeField] private LineRenderer lineRenderer;
        [SerializeField] private ParticleSystem muzzleFlash;
        [SerializeField] private GameObject bloodEffect;

        [Header("Components")]
        [SerializeField] private InputReader inputReader;
        [SerializeField] private Transform gunBarrelTransform;
        
        private LevelManager _levelManager;
        private int _currentLevel;
        private GameObject _crosshair;
        private GunStats _currentGunStats;
        private CancellationTokenSource _cancellationTokenSource;
        
        private bool _isPlayerDead;
        
        private void OnEnable()
        {
            _levelManager = GetComponent<LevelManager>();
            _crosshair = GameObject.FindWithTag("Crosshair");
            if(muzzleFlash.isPlaying) muzzleFlash.Stop();
            playerDeathEvent.OnEventRaised += OnPlayerDeath;
            _isPlayerDead = false;
            inputReader.OnFireStarted += OnFireStart;
            inputReader.OnFireCanceled += OnFireEnd;
        }

        private void OnPlayerDeath()
        {
            _isPlayerDead = true;
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
            playerDeathEvent.OnEventRaised -= OnPlayerDeath;
            inputReader.OnFireStarted -= OnFireStart;
            inputReader.OnFireCanceled -= OnFireEnd;
        }

        private async UniTaskVoid FireGunAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(0.5f), cancellationToken: cancellationToken);
                muzzleFlash.Play();
                Vector3 direction = _crosshair.transform.position - gunBarrelTransform.position;
                if (Physics.Raycast(gunBarrelTransform.position, direction, out RaycastHit hit, _currentGunStats.range))
                {
                    if (hit.collider.TryGetComponent<IDamageable>(out var damagable))
                    {
                        damagable.TakeDamage(_currentGunStats.damage);
                        Instantiate(bloodEffect, hit.point, Quaternion.LookRotation(hit.normal));
                    }   
                }
            }
        }

        private void DrawLineToCrosshair()
        {
            if (_isPlayerDead)
            {
                lineRenderer.enabled = false;
                return;
            }
            lineRenderer.SetPosition(0, gunBarrelTransform.position);
            lineRenderer.SetPosition(1, _crosshair.transform.position);
        }
        
#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            if (_crosshair != null)
            {
                Gizmos.DrawRay(gunBarrelTransform.position, _crosshair.transform.position - gunBarrelTransform.position);
            }
        }
#endif
    }
}