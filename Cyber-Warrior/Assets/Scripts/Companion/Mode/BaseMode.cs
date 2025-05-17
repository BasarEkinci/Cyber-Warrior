using Movement;
using UnityEngine;

namespace Companion.Mode
{
    public class BaseMode : ICmpModeStrategy
    {
        public void SetAimMode(Rotator rotator,GameObject target, float rotationSpeed)
        {
            rotator.SetLookDirection();
            // Default behavior for the base mode
            // This could be a neutral state or a placeholder for future modes
        }

        public void SetProperties(Material eyeMaterial)
        {
            eyeMaterial.color = Color.yellow;            
        }

        public void ModeBehaviour()
        {
            Debug.Log("Base mode behavior executed.");
        }
    }
}