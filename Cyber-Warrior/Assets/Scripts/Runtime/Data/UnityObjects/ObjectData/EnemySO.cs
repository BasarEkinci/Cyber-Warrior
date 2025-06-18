using UnityEngine;

namespace Runtime.Data.UnityObjects.ObjectData
{
    [CreateAssetMenu(fileName = "Enemy", menuName = "Scriptable Objects/Enemy")]
    public class EnemySo : ScriptableObject
    {
        [Header("Enemy Settings")]
        public float damage;
        public float attackInterval;
        public float moveSpeed;
        public float maxHealth;
        public float damageResistance;
        public int xpValue;
    }
}


