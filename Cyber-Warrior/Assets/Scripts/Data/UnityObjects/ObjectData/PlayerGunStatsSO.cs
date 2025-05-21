using System.Collections.Generic;
using Data.ValueObjects;
using UnityEngine;

namespace Data.UnityObjects
{
    [CreateAssetMenu(fileName = "PlayerGun", menuName = "Scriptable Objects/PlayerGun", order = 0)]
    public class PlayerGunStatsSO : ScriptableObject
    {
        public int MaxLevel => GunStatsList.Count - 1;
        public List<GunStats> GunStatsList;
    }
}