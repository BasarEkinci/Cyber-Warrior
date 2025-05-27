using Enums;
using Runtime.Data.ValueObjects;
using UnityEngine;

namespace Runtime.CompanionBot.Mode
{
    public class BaseBotMode : CmpBotMode
    {
        public override GameState ValidGameState => GameState.Base;
        public override Transform TargetObject { get; set; }
        public override Transform FollowPosition { get; set; }
        private CmpBotStatData _botData;
        public override void Initialize()
        {
            TargetObject = anchorPoints.GetInitialTargetObject();
            FollowPosition = anchorPoints.GetAnchorPoint(mode);
            eyeMaterial.color = modeColor;
            _botData = GetDataAtCurrentLevel();
        }
        
        public override void Execute()
        {
        }

        public override void RotateBehaviour(Transform currentTransform)
        {
            currentTransform.LookAt(TargetObject);
        }

        public override void MoveBehaviourFixed(Transform currentTransform)
        {
            Vector3 desiredPosition = FollowPosition.position;
            currentTransform.position = Vector3.Lerp(currentTransform.position, desiredPosition, botData.movementData.moveSpeed * Time.fixedDeltaTime);
        }
        public override CmpBotStatData GetDataAtCurrentLevel()
        {
            return botData.statDataList[levelManager.CurrentLevel];
        }
    }
}