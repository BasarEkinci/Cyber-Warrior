using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "PlayerStats", menuName = "Scriptable Objects/PlayerStats")]
    public class PlayerStats : ScriptableObject
    {
        public float moveSpeed;
        public float rotateSpeed;
        public float maxHealth = 100f;
    }
}
