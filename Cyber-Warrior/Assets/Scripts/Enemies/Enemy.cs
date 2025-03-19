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

        [SerializeField] private float damage = 10f;
        [SerializeField] private float attackInterval = 5f; // Hasar verme aralığı

        private NavMeshAgent _agent;
        private Transform _playerTransform;
        private PlayerHealth _playerHealth;
        private CancellationTokenSource _damageCancellationToken;

        private void OnEnable()
        {
            _agent = GetComponent<NavMeshAgent>();

            var playerManager = FindFirstObjectByType<PlayerManager>();
            if (playerManager != null)
            {
                _playerTransform = playerManager.transform;
            }
        }

        private void Update()
        {
            if (_playerTransform != null)
            {
                _agent.SetDestination(_playerTransform.position);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out _playerHealth))
            {
                _playerHealth.TakeDamage(damage);
                StartDamageLoop();
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.TryGetComponent<PlayerHealth>(out _))
            {
                StopDamageLoop();
            }
        }

        private async void StartDamageLoop()
        {
            _damageCancellationToken?.Cancel();
            _damageCancellationToken = new CancellationTokenSource();

            while (_playerHealth != null && !_damageCancellationToken.Token.IsCancellationRequested)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(attackInterval), cancellationToken: _damageCancellationToken.Token);
                _playerHealth?.TakeDamage(damage);
            }
        }

        private void StopDamageLoop()
        {
            _damageCancellationToken?.Cancel();
        }
    }
}

