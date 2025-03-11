using ScriptableObjects;
using UnityEngine;

namespace Player
{
    public class PlayerHealth
    {
        private readonly HealthEvent _healthEvent;
        private float _currentHealth;
        private float _maxHealth;
        
        public PlayerHealth(float maxHealth)
        {
            _healthEvent = Resources.Load<HealthEvent>("UnityObjects/HealthEvent");
            _maxHealth = maxHealth;
            _currentHealth = maxHealth;
        }
        
        public void TakeDamage(float damage)
        {
            _currentHealth -= damage;
            _currentHealth = Mathf.Max(_currentHealth, 0);
            _healthEvent.Invoke(_currentHealth);
        }
        
        public void Heal(float healAmount)
        {
            _currentHealth += healAmount;
            _currentHealth = Mathf.Min(_currentHealth, _maxHealth);
            _healthEvent.Invoke(_currentHealth);
        }

        public void IncreaseMaxHealth(float amount)
        {
            _maxHealth += amount;
        }
    }
}
