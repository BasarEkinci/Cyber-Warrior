using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Player;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private ScriptableObjects.Enemy enemy;

        private NavMeshAgent _agent;
        private Transform _playerTransform;
        private PlayerHealth _playerHealth;
        private CancellationTokenSource _cancellationToken;

        private void OnEnable()
        {
            _agent = GetComponent<NavMeshAgent>();
            _agent.speed = enemy.moveSpeed;
            var playerManager = FindFirstObjectByType<PlayerManager>();
            if (playerManager != null)
                _playerTransform = playerManager.transform;

            _cancellationToken = new CancellationTokenSource();
            StartDamagingAsync(_cancellationToken);
        }

        private void FixedUpdate()
        {
            if (_playerTransform != null)
                _agent.SetDestination(_playerTransform.position);
        }

        void OnDisable()
        {
            _cancellationToken?.Cancel();
            _cancellationToken?.Dispose();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerHealth playerHealth))
            {
                _playerHealth = playerHealth;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _playerHealth = null;
            }
        }

        private async void StartDamagingAsync(CancellationTokenSource token)
        {
            while (!token.IsCancellationRequested)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(enemy.attackInterval));
                if (_playerHealth != null)
                {
                    _playerHealth.TakeDamage(enemy.damage);
                }
            }
        }

    }
}

