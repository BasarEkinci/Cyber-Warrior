using UnityEngine;

namespace Data.ValueObjects
{
    [System.Serializable]
    public struct CmpCombatData
    {
        public float AttackCooldown;
        public float Damage;
        public LayerMask EnemyLayer;
    }

    [System.Serializable]
    public struct CmpHealerData
    {
        public float HealCooldown;
        public float HealAmount;
    }
    [System.Serializable]
    public struct CmpMovementData
    {
        public Vector3 FollowOffset;
        public Vector3 AttackOffset;
        public float MoveSpeed;
        public float RotationSpeed;
    }
}