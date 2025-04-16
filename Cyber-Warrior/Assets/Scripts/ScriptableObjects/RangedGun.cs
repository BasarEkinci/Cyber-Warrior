using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "RangedGun", menuName = "Scriptable Objects/Guns/RangedGun")]
    public class RangedGun : ScriptableObject
    {
        public float damage;
        public float range;
        public float fireRate;
        public float bulletForce;
        public int capacity;
        public GameObject bulletPrefab;
        [Range(0, 1)] public float knockBackForce;
    }
}
