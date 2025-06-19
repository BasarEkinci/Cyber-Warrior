using Runtime.Data.ValueObjects;
using Runtime.Enums;
using Runtime.Managers;
using Runtime.Player;
using UnityEngine;

namespace Runtime.CompanionBot.Mode
{
    public class HealerBotMode : CmpBotMode
    {
        private PlayerHealth _playerHealth;
        private CmpHealerData _botData;
        private float _timer;
        public override GameState ValidGameState => GameState.Action;
        public override Transform TargetObject { get; set; }
        public override Transform FollowPosition { get; set; }

        public override void Initialize()
        {
            if (anchorPoints == null)
            {
                anchorPoints = FindFirstObjectByType<BotAnchorPoints>();
            }
            if (_playerHealth == null)
            {
                _playerHealth = FindFirstObjectByType<PlayerHealth>();
            }
            TargetObject = anchorPoints.GetInitialTargetObject();
            FollowPosition = anchorPoints.GetAnchorPoint(mode);
            eyeMaterial.color = modeColor;
            _botData = GetDataAtCurrentLevel().healerData;
        }
        
        public override void Execute()
        {
            if (_playerHealth.CurrentHealth <= 0f)
            {
                return;
            }
            _timer += Time.deltaTime;
            if (_timer >= _botData.healCooldown)
            {
                _timer = 0f;
                _playerHealth.Heal(_botData.healAmount);
            }
        }

        public override void ExitState()
        {
        }

        public override void RotateBehaviour(Transform currentTransform)
        {
            currentTransform.LookAt(TargetObject);
        }

        public override void Move(Transform currentTransform, float deltaTime)
        {
            Vector3 desiredPosition = FollowPosition.position;
            currentTransform.position = Vector3.Lerp(currentTransform.position, desiredPosition, botData.movementData.moveSpeed * deltaTime);
        }
        
        private CmpBotStatData GetDataAtCurrentLevel()
        {
            return botData.statDataList[GameDatabaseManager.Instance.LoadData(SaveKeys.CompanionLevel)];
        }
    }
}