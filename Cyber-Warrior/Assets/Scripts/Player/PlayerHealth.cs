using ScriptableObjects.Events;
using UnityEngine;

namespace Player
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private PlayerDeathEventChannelSO playerDeath;
        [SerializeField] private HealthEventChannelSO healthEventChannelSo;
        private float _currentHealth;
        private float _maxHealth = 100;
        private Animator _animator;
        private void OnEnable()
        {
            _animator = GetComponent<Animator>();
            _currentHealth = _maxHealth;
        }

        public void TakeDamage(float damage)
        {
            _currentHealth -= damage;
            _currentHealth = Mathf.Max(_currentHealth, 0);
            healthEventChannelSo.Invoke(_currentHealth);
            if (_currentHealth <= 0f)
            {
                playerDeath.Invoke();
                _animator.Play("Death");
            }
        }

        public void Heal(float healAmount)
        {
            _currentHealth += healAmount;
            _currentHealth = Mathf.Min(_currentHealth, _maxHealth);
            healthEventChannelSo.Invoke(_currentHealth);
        }

        public void IncreaseMaxHealth(float amount)
        {
            _maxHealth += amount;
        }
    }
}
