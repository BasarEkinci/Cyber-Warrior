using ScriptableObjects.Events;
using UnityEngine;

namespace Player
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private PlayerDeathEvent playerDeath;
        [SerializeField] private HealthEvent healthEvent;
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
            healthEvent.Invoke(_currentHealth);
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
            healthEvent.Invoke(_currentHealth);
        }

        public void IncreaseMaxHealth(float amount)
        {
            _maxHealth += amount;
        }
    }
}
