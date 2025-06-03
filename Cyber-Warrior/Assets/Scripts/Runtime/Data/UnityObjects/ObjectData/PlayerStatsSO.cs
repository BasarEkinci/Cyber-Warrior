using System.Collections.Generic;
using Runtime.Data.ValueObjects;
using Runtime.Enums;
using UnityEngine;

namespace Runtime.Data.UnityObjects.ObjectData
{
    [CreateAssetMenu(fileName = "PlayerStats", menuName = "Scriptable Objects/PlayerStats")]
    public class PlayerStatsSO : ScriptableObject
    {
        public ItemType Type => ItemType.Player;
        public int MaxLevel => playerStatsDataList.Count - 1;
        public List<PlayerStatsData> playerStatsDataList;
    }
}
