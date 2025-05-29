using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Data.UnityObjects;
using JetBrains.Annotations;
using Runtime.Data.UnityObjects.Events;
using Runtime.Interfaces;
using Runtime.Player;
using UnityEngine;
using UnityEngine.AI;

namespace Runtime.Enemies
{
    public class Enemy : MonoBehaviour, IDamageable
    {
        public bool IsDead => _currentHealth <= 0f;
        public float CurrentHealth => _currentHealth;
        
        [SerializeField] private GameObject bloodEffect;
        
        [Header("Scriptables")]
        [SerializeField] private VoidEventSO voidEventSo;
        [SerializeField] private EnemySO enemySo;
        [SerializeField] private ScrapTypesSO scrapTypesSo;
        
        private NavMeshAgent _agent;
        private Transform _playerTransform;
        private CancellationTokenSource _cancellationToken;
        private Animator _animator;
        private Collider _collider;
        private float _currentHealth;
        private float _damageResistance;
        private bool _isPLayerDead;
        private static readonly int IsAttached = Animator.StringToHash("IsAttached");

        #region Unity Functions
        private void OnEnable()
        {
            Initialize();
        }

        private void FixedUpdate()
        {
            if (_currentHealth <= 0f) return;
            if (_playerTransform != null || !_isPLayerDead)
                _agent.SetDestination(_playerTransform.position);
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerHealth playerHealth))
            {
                _animator.SetBool(IsAttached, true);
                _cancellationToken = new CancellationTokenSource();
                StartDamagingAsync(_cancellationToken, playerHealth);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _animator.SetBool(IsAttached, false);
                _cancellationToken.Cancel();
                _cancellationToken.Dispose();
            }
        }
        #endregion

        #region Custom Functions
        private async void StartDamagingAsync(CancellationTokenSource token, [CanBeNull] PlayerHealth playerHealth = null)
        {
            while (!token.IsCancellationRequested)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(enemySo.attackInterval));
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(enemySo.damage);
                    Instantiate(bloodEffect, playerHealth.transform.position, Quaternion.LookRotation(playerHealth.transform.forward));
                }
            }
        }

        public void TakeDamage(float amount)
        {
            float damage;
            if (amount > enemySo.damageResistance)
                damage = amount - enemySo.damageResistance;
            else
                damage = amount;
            Instantiate(bloodEffect, transform.position + Vector3.up * 1.5f, Quaternion.LookRotation(transform.forward));
            _currentHealth -= damage;
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
            CreateScarp();
        }
        private void OnPlayerDeath()
        {
            _isPLayerDead = true;
            _animator.Play("idle");
            _cancellationToken.Cancel();
            _cancellationToken.Dispose();
        }
        private void Initialize()
        {
            _agent = GetComponent<NavMeshAgent>();
            _animator = GetComponentInChildren<Animator>();
            _collider = GetComponent<Collider>();
            voidEventSo.OnEventRaised += OnPlayerDeath;
            _agent.speed = enemySo.moveSpeed;
            _currentHealth = enemySo.maxHealth;
            _damageResistance = enemySo.damageResistance;
            var playerManager = FindFirstObjectByType<PlayerManager>();
            if (playerManager != null)
                _playerTransform = playerManager.transform;
        }

        private void CreateScarp()
        {
            GameObject scarp = scrapTypesSo.GetRandomScrap();
            Instantiate(scarp, transform.position, Quaternion.identity);
        }
        #endregion
    }
}

