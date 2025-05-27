using Enums;
using Player;
using Runtime.Data.ValueObjects;
using UnityEngine;

namespace Runtime.CompanionBot.Mode
{
    public class HealerBotMode : CmpBotMode
    {
        [SerializeField] private PlayerHealth playerHealth;
        private CmpHealerData _botData;
        private float _timer;
        public override GameState ValidGameState => GameState.Action;

        public override void Initialize()
        {
            eyeMaterial.color = modeColor;
            _botData = GetDataAtCurrentLevel().healerData;
        }
        
        public override void Execute()
        {
            _timer += Time.deltaTime;
            if (_timer >= _botData.healCooldown)
            {
                _timer = 0f;
                playerHealth.Heal(_botData.healAmount);
            }
        }

        public override void RotateBehaviour(Transform currentTransform)
        {
            currentTransform.LookAt(targetObject);
        }

        public override void MoveBehaviourFixed(Transform currentTransform)
        {
            Vector3 desiredPosition = followPosition.position;
            currentTransform.position = Vector3.Lerp(currentTransform.position, desiredPosition, botData.movementData.moveSpeed * Time.fixedDeltaTime);
        }
        
        public override CmpBotStatData GetDataAtCurrentLevel()
        {
            return botData.statDataList[levelManager.CurrentLevel];
        }
    }
}