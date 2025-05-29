using Data.UnityObjects;
using Data.UnityObjects.Events;
using Runtime.Data.UnityObjects.Events;
using Runtime.Data.ValueObjects;
using UnityEngine;

namespace Runtime.Player
{
    public class PlayerHealth : MonoBehaviour
    {
        public float CurrentHealth => _currentHealth;
        public float MaxHealth => _playerStatsData.maxHealth;
        [SerializeField] private VoidEventSO playerDeathEvent;
        [SerializeField] private PlayerStatsSO playerStatsSo;
        [SerializeField] private FloatEventChannel healthEventSO;
        
        private int _currentLevel;
        private float _currentHealth;
        private Animator _animator;
        private PlayerStatsData _playerStatsData;
        private void OnEnable()
        {
            _animator = GetComponent<Animator>();
            _playerStatsData = playerStatsSo.playerStatsDataList[_currentLevel];
            _currentHealth = _playerStatsData.maxHealth;
        }

        public void TakeDamage(float damage)
        {
            _currentHealth -= damage;
            _currentHealth = Mathf.Max(_currentHealth, 0);
            healthEventSO.OnEventRaised(_currentHealth);
            if (_currentHealth <= 0f)
            {
                playerDeathEvent?.Invoke();
                _animator.Play("Death");
            }
        }

        public void Heal(float healAmount)
        {
            if (_currentHealth >= _playerStatsData.maxHealth)
            {
                return;
            }
            _currentHealth += healAmount;
            _currentHealth = Mathf.Min(_currentHealth, _playerStatsData.maxHealth);
            healthEventSO.OnEventRaised(_currentHealth);
        }

        public void Upgrade(int amount)
        {
            if (_currentLevel >= playerStatsSo.MaxLevel)
            {
                return;
            }
            _currentLevel += amount;
        }
    }
}
