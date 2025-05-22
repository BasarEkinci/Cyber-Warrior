using Movement;
using UnityEngine;

namespace CompanionBot.Mode
{
    public class BaseBotMode : ICmpBotModeStrategy
    {
        public void Execute(Rotator rotator,GameObject reference, float rotationSpeed)
        {
            rotator.SetLookDirection();
        }

        public void SetProperties(Material eyeMaterial)
        {
            eyeMaterial.color = Color.cyan;            
        }
        public void ModeBehaviour()
        {
        }
    }
}