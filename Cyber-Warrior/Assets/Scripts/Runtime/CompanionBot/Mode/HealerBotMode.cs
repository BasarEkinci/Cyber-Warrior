using Data.ValueObjects;
using Enums;
using Player;
using UnityEngine;

namespace Runtime.CompanionBot.Mode
{
    public class HealerBotMode : CmpBotMode
    {
        [SerializeField] private PlayerHealth playerHealth;
        private CmpBotStatData _botData;
        private float _timer;
        public override GameState ValidGameState => GameState.Action;

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

        public override void RotateBehaviour(Transform currentTransform)
        {
            currentTransform.LookAt(targetObject);
        }

        public override void MoveBehaviourFixed(Transform currentTransform)
        {
            Vector3 desiredPosition = followPosition.position;
            currentTransform.position = Vector3.Lerp(currentTransform.position, desiredPosition, botData.movementData.MoveSpeed * Time.fixedDeltaTime);
        }
    }
}