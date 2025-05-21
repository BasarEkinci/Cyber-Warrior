using UnityEngine;

namespace Data.UnityObjects
{
    [CreateAssetMenu(fileName = "PlayerStats", menuName = "Scriptable Objects/PlayerStats")]
    public class PlayerStatsSO : ScriptableObject
    {
        public float moveSpeed;
        public float rotateSpeed;
        public float maxHealth = 100f;
    }
}
