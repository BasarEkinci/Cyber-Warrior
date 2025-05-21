using UnityEngine;

namespace Data.UnityObjects
{
    [CreateAssetMenu(fileName = "Enemy", menuName = "Scriptable Objects/Enemy")]
    public class EnemySO : ScriptableObject
    {
        public float damage;
        public float attackInterval;
        public float moveSpeed;
        public float maxHealth;
        public float damageResistance;
    }
}


