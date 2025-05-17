using Companion.Mode;
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

        public void SetAimMode(Rotator rotator,GameObject target, float rotationSpeed)
        {
            rotator.SetLookDirection();
            //Heal Logic Here
        }

        public void SetProperties(Material eyeMaterial)
        {
            eyeMaterial.color = Color.green;             
            // Set properties specific to the healer mode
            // For example, change the color of the eye material to indicate healing mode
            // This could also involve changing other visual elements or effects
        }

        public void ModeBehaviour()
        {
            _cooldownTimer += Time.deltaTime;
            if (_cooldownTimer >= _healCooldown)
            {
                if (_playerHealth != null)
                {
                    Debug.Log("Healing player");
                    _playerHealth.Heal(_healAmount);
                }
                _cooldownTimer = 0f;
            }
        }
    }
}