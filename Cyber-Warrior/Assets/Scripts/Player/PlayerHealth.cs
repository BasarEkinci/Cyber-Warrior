using Data.UnityObjects;
using UnityEngine;

namespace Player
{
    public class PlayerHealth : MonoBehaviour
    {
        public float CurrentHealth => _currentHealth;
        public float MaxHealth => _playerStatsData.maxHealth;
        [SerializeField] private VoidEventSO playerDeathEvent;
        [SerializeField] private FloatEventChannelSO floatEventChannelSo;
        [SerializeField] private PlayerStatsSO playerStatsSo;
        
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
            floatEventChannelSo.Invoke(_currentHealth);
            if (_currentHealth <= 0f)
            {
                playerDeathEvent?.Invoke();
                _animator.Play("Death");
            }
        }

        public void Heal(float healAmount)
        {
            _currentHealth += healAmount;
            _currentHealth = Mathf.Min(_currentHealth, _playerStatsData.maxHealth);
            floatEventChannelSo.Invoke(_currentHealth);
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
