using Data.UnityObjects.Events;
using Runtime.Audio;
using Runtime.Data.UnityObjects.Events;
using Runtime.Data.UnityObjects.ObjectData;
using Runtime.Data.ValueObjects;
using Runtime.Enums;
using UnityEngine;

namespace Runtime.Player
{
    public class PlayerHealth : MonoBehaviour
    {
        public float CurrentHealth => _currentHealth;
        public float MaxHealth => _playerStatsData.maxHealth;
        [SerializeField] private VoidEventSO playerDeathEvent;
        [SerializeField] private PlayerStatsSO playerStatsSo;
        [SerializeField] private GameStateEvent gameStateEvent;
        [SerializeField] private FloatEventChannel healthEventSO;
        
        private int _currentLevel;
        private float _currentHealth;
        private AudioSource _audioSource;
        private Animator _animator;
        private PlayerStatsData _playerStatsData;
        private void OnEnable()
        {
            _animator = GetComponent<Animator>();
            _audioSource = GetComponent<AudioSource>();
            _playerStatsData = playerStatsSo.playerStatsDataList[_currentLevel];
            _currentHealth = _playerStatsData.maxHealth;
        }

        public void TakeDamage(float damage)
        {
            _currentHealth -= damage;
            _currentHealth = Mathf.Max(_currentHealth, 0);
            healthEventSO.OnEventRaised(_currentHealth);
            AudioManager.Instance.PlaySfx(SfxType.PlayerDamage, _audioSource);
            if (_currentHealth <= 0f)
            {
                playerDeathEvent?.Invoke();
                gameStateEvent.RaiseEvent(GameState.GameOver);
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
            AudioManager.Instance.PlaySfx(SfxType.BotHeal,_audioSource);
            _currentHealth = Mathf.Min(_currentHealth, _playerStatsData.maxHealth);
            healthEventSO.OnEventRaised(_currentHealth);
        }
    }
}
