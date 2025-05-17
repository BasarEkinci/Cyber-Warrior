using Movement;
using UnityEngine;

namespace CompanionBot.Mode
{
    public interface ICmpBotModeStrategy
    {
        void SetAimMode(Rotator rotator,GameObject target, float rotationSpeed);
        void SetProperties(Material eyeMaterial);
        void ModeBehaviour();
    }
}