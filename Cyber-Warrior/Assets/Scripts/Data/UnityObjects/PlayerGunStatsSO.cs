using System.Collections.Generic;
using UnityEngine;

namespace Data.UnityObjects
{
    [CreateAssetMenu(fileName = "PlayerGun", menuName = "Scriptable Objects/PlayerGun", order = 0)]
    public class PlayerGunStatsSO : ScriptableObject
    {
        public int MaxLevel => GunStatsList.Count - 1;
        public List<GunStats> GunStatsList;
    }
    
    [System.Serializable]
    public struct GunStats
    {
        public float Damage;
        public float Range;
        public float AttackInterval;
        public int Price;
    }
}