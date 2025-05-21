using UnityEngine;

namespace Data.UnityObjects
{
    [CreateAssetMenu(fileName = "PlayerGun", menuName = "Scriptable Objects/PlayerGun", order = 0)]
    public class PlayerGunStatsSO : ScriptableObject
    {
        public float damage;
        public float attackInterval;
        public float range;
    }
}