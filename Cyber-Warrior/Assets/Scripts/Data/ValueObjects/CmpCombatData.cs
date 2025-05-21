using UnityEngine;

namespace Data.ValueObjects
{
    [System.Serializable]
    public struct CmpCombatData
    {
        public float AttackCooldown;
        public float Damage;
        public float MoveSpeed;
        public float RotationSpeed;
        public Vector3 FollowOffset;
        public Vector3 AttackOffset;
        public LayerMask EnemyLayer;
    }
}