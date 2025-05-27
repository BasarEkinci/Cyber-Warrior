using UnityEngine.Serialization;

namespace Runtime.Data.ValueObjects
{
    [System.Serializable]
    public struct GunStats
    {
        public float damage;
        public float range;
        public float attackInterval;
        public int price;
    }
}