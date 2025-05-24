using Data.ValueObjects;
using Player;
using UnityEngine;

namespace CompanionBot.Mode
{
    public class HealerBotMode : CmpBotMode
    {
        [SerializeField] private Transform followTarget;
        [SerializeField] private PlayerHealth playerHealth;

        private CmpBotStatData _botData;
        private float _timer;
        public override void Initialize()
        {
            eyeMaterial.color = modeColor;
            _botData = botData.statDataList[levelManager.CurrentLevel];
        }
        
        public override void Execute()
        {
            _timer += Time.deltaTime;
            if (_timer >= _botData.HealerData.HealCooldown)
            {
                _timer = 0f;
                playerHealth.Heal(_botData.HealerData.HealAmount);
            }
        }
    }
}