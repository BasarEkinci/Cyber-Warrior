using UnityEngine;

namespace Runtime.Data.ValueObjects
{
    [System.Serializable]
    public struct CmpCombatData
    {
        public float attackCooldown;
        public float damage;
        public float range;
        public LayerMask enemyLayer;
    }
}