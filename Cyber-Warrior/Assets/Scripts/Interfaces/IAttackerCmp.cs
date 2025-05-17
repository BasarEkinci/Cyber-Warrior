using UnityEngine;

namespace Interfaces
{
    public interface IAttackerCmp
    {
        void SetAttackerProperties(Transform companionTransform,LayerMask layerMask);
        Transform FindClosestEnemy();
        Transform GetEnemyTarget();
    }
}