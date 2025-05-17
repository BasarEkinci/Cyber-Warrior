using ScriptableObjects;
using ScriptableObjects.Events;
using UnityEngine;

namespace Player
{
    public class PlayerHealth : MonoBehaviour
    {
        public float CurrentHealth => _currentHealth;

        [SerializeField] private VoidEventSO playerDeathEvent;
        [SerializeField] private FloatEventChannelSO floatEventChannelSo;
        [SerializeField] private PlayerStats playerStats;
        
        
        private float _currentHealth;
        private Animator _animator;
        private void OnEnable()
        {
            _animator = GetComponent<Animator>();
            _currentHealth = playerStats.maxHealth;
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
            _currentHealth = Mathf.Min(_currentHealth, playerStats.maxHealth);
            floatEventChannelSo.Invoke(_currentHealth);
        }

        public void IncreaseMaxHealth(float amount)
        {
            //_maxHealth += amount;
        }
    }
}
