using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Enemies;
using Inputs;
using Interfaces;
using ScriptableObjects;
using ScriptableObjects.Events;
using UnityEngine;

namespace Combat.Components
{
    public class PlayerGun : MonoBehaviour
    {   
        /// <summary>
        /// Test value
        /// </summary>
        [SerializeField] private float range;
        
        [SerializeField] private Transform gunBarrelTransform;
        
        [SerializeField] private HoldInputChannelSO holdInputChannelSo;
        [SerializeField] private PlayerGunBaseStats playerGunBaseStats;
        
        [SerializeField] private LineRenderer lineRenderer;
        [SerializeField] private ParticleSystem muzzleFlash;
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private GameObject bloodEffect;
        
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
                await UniTask.Delay(TimeSpan.FromSeconds(0.5f), cancellationToken: cancellationToken);
                muzzleFlash.Play();
                Vector3 direction = (_crosshair.transform.position - gunBarrelTransform.position) * playerGunBaseStats.range;
                if (Physics.Raycast(gunBarrelTransform.position, direction, out RaycastHit hit, playerGunBaseStats.range))
                {
                    if (hit.collider.TryGetComponent<IDamagable>(out var damagable))
                    {
                        damagable.GetDamage(playerGunBaseStats.damage);
                        Instantiate(bloodEffect, hit.point, Quaternion.LookRotation(hit.normal));
                        Debug.Log(hit.collider.GetComponent<Enemy>().CurrentHealth);
                    }   
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            if (_crosshair != null)
            {
                Gizmos.DrawRay(gunBarrelTransform.position, _crosshair.transform.position - gunBarrelTransform.position);
            }
        }

        private void DrawLineToCrosshair()
        {
            lineRenderer.SetPosition(0, gunBarrelTransform.position);
            lineRenderer.SetPosition(1, _crosshair.transform.position);
        }
    }
}