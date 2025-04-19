using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Enemy", menuName = "Scriptable Objects/Enemy")]
    public class Enemy : ScriptableObject
    {
        public float damage;
        public float attackInterval;
        public float moveSpeed;
        public float maxHealth;
        public float damageResistance;
    }
}


