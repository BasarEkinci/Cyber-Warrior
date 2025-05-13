using ScriptableObjects.Events;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private VoidEventSO playerDeathEvent;
        [SerializeField] private FloatEventChannelSO floatEventChannelSo;
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
            floatEventChannelSo.Invoke(_currentHealth);
            if (_currentHealth <= 0f)
            {
                playerDeathEvent.Invoke();
                _animator.Play("Death");
            }
        }

        public void Heal(float healAmount)
        {
            _currentHealth += healAmount;
            _currentHealth = Mathf.Min(_currentHealth, _maxHealth);
            floatEventChannelSo.Invoke(_currentHealth);
        }

        public void IncreaseMaxHealth(float amount)
        {
            _maxHealth += amount;
        }
    }
}
