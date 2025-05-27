using System.Collections.Generic;
using Runtime.Data.ValueObjects;
using UnityEngine;

namespace Data.UnityObjects
{
    [CreateAssetMenu(fileName = "PlayerStats", menuName = "Scriptable Objects/PlayerStats")]
    public class PlayerStatsSO : ScriptableObject
    {
        public int MaxLevel => playerStatsDataList.Count - 1;
        public List<PlayerStatsData> playerStatsDataList;
    }
}
