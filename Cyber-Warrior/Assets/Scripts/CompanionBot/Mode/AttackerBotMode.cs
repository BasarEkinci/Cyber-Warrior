using UnityEngine;

namespace CompanionBot.Mode
{
    public class AttackerBotMode : CmpBotMode
    {
        public override void Initialize()
        {
            Debug.Log("AttackerBotMode initialized.");
        }

        public override void Execute()
        {
            Debug.Log("AttackerBotMode executing.");
        }
    }
}