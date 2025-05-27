using Enums;
using UnityEngine;

namespace Runtime.CompanionBot.Mode
{
    public class BaseBotMode : CmpBotMode
    {
        public override GameState ValidGameState => GameState.Base;

        public override void Initialize()
        {
            Debug.Log("BaseBotMode initialized.");
        }

        public override void Execute()
        {
            Debug.Log("BaseBotMode executing.");
        }

        public override void RotateBehaviour(Transform currentTransform)
        {
            throw new System.NotImplementedException();
        }

        public override void MoveBehaviourFixed(Transform currentTransform)
        {
            throw new System.NotImplementedException();
        }
    }
}