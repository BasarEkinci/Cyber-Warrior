using UnityEngine;

namespace CompanionBot.Mode
{
    public class BaseBotMode : CmpBotMode
    {
        public override void Initialize()
        {
            Debug.Log("BaseBotMode initialized.");
        }

        public override void Execute()
        {
            Debug.Log("BaseBotMode executing.");
        }
    }
}