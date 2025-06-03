using System.Collections.Generic;
using Runtime.Data.ValueObjects;
using Runtime.Enums;
using UnityEngine;

namespace Runtime.Data.UnityObjects.ObjectData
{
    [CreateAssetMenu(fileName = "PlayerGun", menuName = "Scriptable Objects/PlayerGun", order = 0)]
    public class PlayerGunStatsSO : ScriptableObject
    {
        public ItemType Type => ItemType.Gun;
        public int MaxLevel => GunStatsList.Count - 1;
        public List<GunStats> GunStatsList;
    }
}