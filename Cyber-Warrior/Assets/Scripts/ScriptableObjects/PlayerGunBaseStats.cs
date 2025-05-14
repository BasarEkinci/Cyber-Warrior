using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "PlayerGun", menuName = "Scriptable Objects/PlayerGun", order = 0)]
    public class PlayerGunBaseStats : ScriptableObject
    {
        public float damage;
        public float attackInterval;
        public float range;
    }
}