using Movement;
using Player;
using UnityEngine;

namespace CompanionBot.Mode
{
    public class HealerBotMode : ICmpBotModeStrategy
    {
        private float _healCooldown = 1f;
        private float _cooldownTimer;
        private float _healAmount = 1f;
        private PlayerHealth _playerHealth;
        public HealerBotMode()
        {
            _playerHealth = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();
        }

        public void Execute(Rotator rotator,GameObject reference, float rotationSpeed)
        {
            rotator.SetLookDirection();
        }

        public void SetProperties(Material eyeMaterial)
        {
            eyeMaterial.color = Color.green;             
        }

        public void ModeBehaviour()
        {
            if (_playerHealth.CurrentHealth  >= _playerHealth.MaxHealth)
            {
                return;
            }
            _cooldownTimer += Time.deltaTime;
            if (_cooldownTimer >= _healCooldown)
            {
                if (_playerHealth != null)
                {
                    _playerHealth.Heal(_healAmount);
                }
                _cooldownTimer = 0f;
            }
        }
    }
}