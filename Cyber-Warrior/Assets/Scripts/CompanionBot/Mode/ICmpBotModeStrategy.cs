using Movement;
using UnityEngine;

namespace CompanionBot.Mode
{
    public interface ICmpBotModeStrategy
    {
        void Execute(Rotator rotator,GameObject reference, float rotationSpeed);
        void SetProperties(Material eyeMaterial);
        void ModeBehaviour();
    }
}