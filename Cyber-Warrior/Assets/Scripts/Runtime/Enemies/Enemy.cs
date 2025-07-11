using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using Runtime.Audio;
using Runtime.Data.UnityObjects.Events;
using Runtime.Data.UnityObjects.ObjectData;
using Runtime.Enums;
using Runtime.Gameplay;
using Runtime.Interfaces;
using Runtime.Player;
using UnityEngine;

namespace Runtime.Enemies
{
    public class Enemy : MonoBehaviour, IDamageable
    {
        public VoidEventSO EnemyDeathEvent => enemyDeathEvent;
        
        [Header("Visuals")]
        [SerializeField] private GameObject bloodEffect;
        
        [Header("Scriptables")]
        [SerializeField] private VoidEventSO playerDeathEvent;
        [SerializeField] private VoidEventSO enemyDeathEvent;
        [SerializeField] private VoidEventSO levelSuccessEvent;
        [SerializeField] private GameStateEvent gameStateEvent;
        [SerializeField] private EnemySo enemySo;
        [SerializeField] private ScrapTypesSO scrapTypesSo;
        
        private EnemyPool _enemyPool;
        private EnemyTickManager _enemyTickManager;
        private PlayerHealth _playerHealth;
        private AudioSource _audioSource;
        private Transform _target;
        private Vector3 _moveDirection;
        private CancellationTokenSource _cancellationToken;
        private Animator _animator;
        private Collider _collider;
        private float _currentHealth;
        private float _damageResistance;
        private bool _isPLayerDead;
        private bool _canMove = true;
        private static readonly int IsAttached = Animator.StringToHash("IsAttached");

        #region Unity Functions
        private void OnEnable()
        {
            Initialize();
        }

        private void FixedUpdate()
        {
            if (_currentHealth <= 0f) return;
            if (_isPLayerDead) return;
            if (!_canMove) return;
            transform.position += _moveDirection * (enemySo.moveSpeed * Time.fixedDeltaTime);
            RotateTowardsTarget();
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerHealth playerHealth))
            {
                _animator.SetBool(IsAttached, true);
                _playerHealth = playerHealth;
                _cancellationToken = new CancellationTokenSource();
                StartDamagingAsync(_cancellationToken,playerHealth);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _animator.SetBool(IsAttached, false);
                _playerHealth = null;
                _cancellationToken.Cancel();
                _cancellationToken.Dispose();
            }
        }

        private void OnDisable()
        {
             _enemyTickManager.Unregister(this);
             playerDeathEvent.OnEventRaised -= OnPlayerDeath;
             levelSuccessEvent.OnEventRaised -= OnLevelSucceed;
             gameStateEvent.OnEventRaised -= OnStateChange;
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
            AudioManager.Instance.PlaySfx(SfxType.EnemyDamage, _audioSource);
            Instantiate(bloodEffect, transform.position + Vector3.up * 1.5f, Quaternion.LookRotation(transform.forward));
            _currentHealth -= damage;
            if (_currentHealth <= 0f)
            {
                Dead();
            }
        }
        public async void Dead()
        {
            AudioManager.Instance.PlaySfx(SfxType.EnemyDamage, _audioSource);
            _collider.enabled = false;
            enemyDeathEvent.OnEventRaised();
            _animator.Play("Death1");
            CreateScarp();
            await UniTask.Delay(TimeSpan.FromSeconds(2f));
            _enemyPool.ReturnEnemy(this);
        }
        private async void OnPlayerDeath()
        {
            _isPLayerDead = true;
            _playerHealth = null;
            _animator.Play("idle");
            if (_cancellationToken != null && !_cancellationToken.IsCancellationRequested)
            {
                _cancellationToken.Cancel();
                _cancellationToken.Dispose();   
            }
            await UniTask.Delay(TimeSpan.FromSeconds(2f));
            _enemyPool.ReturnEnemy(this);
        }
        private void Initialize()
        {
            _animator = GetComponentInChildren<Animator>();
            _collider = GetComponent<Collider>();
            _enemyPool = FindFirstObjectByType<EnemyPool>();
            _enemyTickManager = FindFirstObjectByType<EnemyTickManager>();
            _audioSource = GetComponent<AudioSource>();
            playerDeathEvent.OnEventRaised += OnPlayerDeath;
            gameStateEvent.OnEventRaised += OnStateChange;
            levelSuccessEvent.OnEventRaised += OnLevelSucceed;
            _currentHealth = enemySo.maxHealth;
            _damageResistance = enemySo.damageResistance;
            AudioManager.Instance.SetClip(SfxType.EnemyNoise, _audioSource, true);
        }

        private void OnLevelSucceed()
        {
            Dead();
        }

        private void OnStateChange(GameState arg0)
        {
            if (arg0 != GameState.Action)
            {
                _canMove = false;
                if (_playerHealth != null)
                {
                    _playerHealth = null;
                    _cancellationToken.Cancel();
                    _cancellationToken.Dispose();   
                }
            }
            else
            {
                _canMove = true;
            }
        }

        private void CreateScarp()
        {
            GameObject scarp = scrapTypesSo.GetRandomScrap();
            Instantiate(scarp, transform.position, Quaternion.identity);
        }

        public void InitializeValues(Transform player)
        {
            _target = player;
            _enemyTickManager.Register(this);
        }

        public void Tick()
        {
            if (_target == null) return;
            Vector3 direction = (_target.position - transform.position).normalized;
            _moveDirection = new Vector3(direction.x, 0f, direction.z);
        }

        private void RotateTowardsTarget()
        {
            if (_target == null || _isPLayerDead) return;
            Vector3 direction = (_target.position - transform.position).normalized;
            direction.y = 0f;
            if (direction == Vector3.zero) return;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * 10f);
        }
        
        #endregion
    }
}

