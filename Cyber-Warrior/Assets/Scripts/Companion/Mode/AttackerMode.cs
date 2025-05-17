using Movement;
using UnityEngine;

namespace Companion.Mode
{
    public class AttackerMode : ICmpModeStrategy
    {
        public void SetAimMode(Rotator rotator,GameObject target, float rotationSpeed)
        {
            rotator.RotateToTarget(target, rotationSpeed);
            // Add attack logic here
            // For example, check if the target is within a certain range and perform an attack
            // This could involve dealing damage, playing an animation, etc.
        }

        public void SetProperties(Material eyeMaterial)
        {
            eyeMaterial.color = Color.red;             
        }

        public void ModeBehaviour()
        {
            Debug.Log("Attacker mode behavior executed.");
        }
    }
}