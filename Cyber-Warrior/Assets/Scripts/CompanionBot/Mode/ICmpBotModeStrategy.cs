using Movement;
using UnityEngine;

namespace CompanionBot.Mode
{
    public interface ICmpBotModeStrategy
    {
        void Execute(Rotator rotator,GameObject target, float rotationSpeed);
        void SetProperties(Material eyeMaterial);
    }
}