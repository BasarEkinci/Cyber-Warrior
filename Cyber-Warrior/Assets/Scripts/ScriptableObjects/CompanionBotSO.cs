using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Companion Bot", menuName = "Scriptable Objects/Companion Bot", order = 0)]
    public class CompanionBotSO : ScriptableObject
    {
        public float attackCooldown;
        public float damage;
        public float moveSpeed;
        public float rotationSpeed;
        public LayerMask enemyLayer;
        public Material eyeMaterial;
        public Vector3 followOffset;
        public Vector3 attackOffset;
    }
}