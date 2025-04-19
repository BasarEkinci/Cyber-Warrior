using System;
using System.Threading;
using Combat.Interfaces;
using Cysharp.Threading.Tasks;
using ScriptableObjects;
using Player;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    public class Enemy : MonoBehaviour, IDamagable
    {
        public float CurrentHealth => _currentHealth;
        [SerializeField] private EnemySO enemy;
        [SerializeField] private EnemyDeathEvent deathEvent;
        private NavMeshAgent _agent;
        private Transform _playerTransform;
        private PlayerHealth _playerHealth;
        private CancellationTokenSource _cancellationToken;
        private Animator _animator;
        private Collider _collider;
        private float _currentHealth;
        private float _damageResistance;

        #region Unity Functions
        private void OnEnable()
        {
            Initialize();
        }

        private void FixedUpdate()
        {
            if (_currentHealth <= 0f) return;
            if (_playerTransform != null)
                _agent.SetDestination(_playerTransform.position);
        }

        private void OnDisable()
        {
            _cancellationToken?.Cancel();
            _cancellationToken?.Dispose();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerHealth playerHealth))
            {
                _playerHealth = playerHealth;
                _animator.SetBool("IsAttached", true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _playerHealth = null;
                _animator.SetBool("IsAttached", false);
            }
        }
        #endregion

        #region Custom Functions
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

        public void GetDamage(float amount)
        {
            //_material.DOColor(Color.white, 0.1f).SetLoops(2, LoopType.Yoyo).SetEase(Ease.Linear);
            float damage;
            if (amount > enemy.damageResistance)
                damage = amount - enemy.damageResistance;
            else
                damage = amount;

            _currentHealth -= damage;
            Debug.Log(_currentHealth);
            if (_currentHealth <= 0f)
            {
                Dead();
            }
        }

        public void Dead()
        {
            _collider.enabled = false;
            _agent.enabled = false;
            _animator.Play("Death1");
            deathEvent.Invoke(gameObject);
        }
        private void Initialize()
        {
            _agent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();
            _collider = GetComponent<Collider>();
            _agent.speed = enemy.moveSpeed;
            _currentHealth = enemy.maxHealth;
            _damageResistance = enemy.damageResistance;
            var playerManager = FindFirstObjectByType<PlayerManager>();
            if (playerManager != null)
                _playerTransform = playerManager.transform;

            _cancellationToken = new CancellationTokenSource();
            StartDamagingAsync(_cancellationToken);
        }
        #endregion

    }
}

