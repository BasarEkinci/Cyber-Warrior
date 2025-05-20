using Movement;
using UnityEngine;

namespace CompanionBot.Mode
{
    public class BaseBotMode : ICmpBotModeStrategy
    {
        public void Execute(Rotator rotator,GameObject reference, float rotationSpeed)
        {
            rotator.SetLookDirection();
            // Default behavior for the base mode
            // This could be a neutral state or a placeholder for future modes
        }

        public void SetProperties(Material eyeMaterial)
        {
            eyeMaterial.color = Color.cyan;            
        }

        public void ModeBehaviour()
        {
            Debug.Log("Base mode behavior executed.");
        }
    }
}