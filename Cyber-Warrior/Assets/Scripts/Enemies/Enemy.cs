using System;
using System.Threading;
using Combat.Interfaces;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Player;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    public class Enemy : MonoBehaviour, IDamagable, IKnockbackable
    {
        [SerializeField] private ScriptableObjects.Enemy enemy;

        private NavMeshAgent _agent;
        private Transform _playerTransform;
        private PlayerHealth _playerHealth;
        private CancellationTokenSource _cancellationToken;
        private Animator _animator;

        #region Unity Functions
        private void OnEnable()
        {
            Initialize();
        }

        private void FixedUpdate()
        {
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
        }

        public void Dead()
        {
            throw new NotImplementedException();
        }

        public void Knockback(Vector3 direction, float force)
        {
            Debug.Log("Knockback");
            Vector3 horizontalDirection = new Vector3(direction.x, 0, direction.z).normalized;
            Vector3 targetPos = transform.position + horizontalDirection * force;
            transform.DOMove(targetPos, 0.1f).SetEase(Ease.OutExpo);
        }

        private void Initialize()
        {
            _agent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();
            _agent.speed = enemy.moveSpeed;
            var playerManager = FindFirstObjectByType<PlayerManager>();
            if (playerManager != null)
                _playerTransform = playerManager.transform;

            _cancellationToken = new CancellationTokenSource();
            StartDamagingAsync(_cancellationToken);
        }
        #endregion

    }
}

