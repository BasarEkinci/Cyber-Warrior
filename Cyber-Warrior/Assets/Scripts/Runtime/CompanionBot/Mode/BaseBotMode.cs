using Data.ValueObjects;
using Enums;
using UnityEngine;

namespace Runtime.CompanionBot.Mode
{
    public class BaseBotMode : CmpBotMode
    {
        public override GameState ValidGameState => GameState.Base;
        private CmpBotStatData _botData;
        public override void Initialize()
        {
            eyeMaterial.color = modeColor;
            _botData = botData.statDataList[levelManager.CurrentLevel];
        }
        
        public override void Execute()
        {
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