using UnityEngine;

namespace CompanionBot.Mode
{
    public interface IAttackerCmp
    {
        void SetAttackerProperties(Transform companionTransform,LayerMask layerMask,float damage,float cooldown);
        Transform FindClosestEnemy();
        Transform GetEnemyTarget();
        void Attack(Transform target);
        void SetAttackEffect(ParticleSystem effect);
        void PlayAttackEffect();
    }
}