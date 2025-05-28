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
        public override Transform TargetObject { get; set; }
        public override Transform FollowPosition { get; set; }

        public override void Initialize()
        {
            TargetObject = anchorPoints.GetInitialTargetObject();
            FollowPosition = anchorPoints.GetAnchorPoint(mode);
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
        
        public override CmpBotStatData GetDataAtCurrentLevel()
        {
            return botData.statDataList[levelManager.CurrentLevel];
        }
    }
}