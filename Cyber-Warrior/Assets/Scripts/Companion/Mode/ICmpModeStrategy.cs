using Movement;
using UnityEngine;

namespace Companion.Mode
{
    public interface ICmpModeStrategy
    {
        void SetAimMode(Rotator rotator,GameObject target, float rotationSpeed);
        void SetProperties(Material eyeMaterial);

        void ModeBehaviour();
    }
}