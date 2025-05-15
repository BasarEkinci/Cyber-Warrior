using System;
using System.Threading;
using Combat.Interfaces;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using ScriptableObjects;
using Player;
using UnityEngine;
using UnityEngine.AI;
using ScriptableObjects.Events;

namespace Enemies
{
    public class Enemy : MonoBehaviour, IDamagable
    {
        public float CurrentHealth => _currentHealth;
        
        [SerializeField] private GameObject bloodEffect;
        
        [Header("Scriptables")]
        [SerializeField] private VoidEventSO voidEventSo;
        [SerializeField] private EnemySO enemy;
        
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
                await UniTask.Delay(TimeSpan.FromSeconds(enemy.attackInterval));
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(enemy.damage);
                    Instantiate(bloodEffect, playerHealth.transform.position, Quaternion.LookRotation(playerHealth.transform.forward));
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
        }
        private void OnPlayerDeath()
        {
            _isPLayerDead = true;
            _animator.Play("idle");
        }
        private void Initialize()
        {
            _agent = GetComponent<NavMeshAgent>();
            _animator = GetComponentInChildren<Animator>();
            _collider = GetComponent<Collider>();
            voidEventSo.OnEventRaised += OnPlayerDeath;
            _agent.speed = enemy.moveSpeed;
            _currentHealth = enemy.maxHealth;
            _damageResistance = enemy.damageResistance;
            var playerManager = FindFirstObjectByType<PlayerManager>();
            if (playerManager != null)
                _playerTransform = playerManager.transform;
        }
        #endregion
    }
}

