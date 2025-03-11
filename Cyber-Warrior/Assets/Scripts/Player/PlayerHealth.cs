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
            _healthEvent.Invoke(_currentHealth);
        }
        
        public void Heal(float healAmount)
        {
            _currentHealth += healAmount;
            _healthEvent.Invoke(_currentHealth);
        }

        public void IncreaseMaxHealth(float amount)
        {
            _maxHealth += amount;
        }
    }
}
