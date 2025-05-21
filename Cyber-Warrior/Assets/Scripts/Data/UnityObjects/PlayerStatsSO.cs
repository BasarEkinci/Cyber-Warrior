using System.Collections.Generic;
using UnityEngine;

namespace Data.UnityObjects
{
    [CreateAssetMenu(fileName = "PlayerStats", menuName = "Scriptable Objects/PlayerStats")]
    public class PlayerStatsSO : ScriptableObject
    {
        public int MaxLevel => playerStatsDataList.Count - 1;
        public List<PlayerStatsData> playerStatsDataList;
    }

    [System.Serializable]
    public struct PlayerStatsData
    {
        public int LevelPrice;
        public float moveSpeed;
        public float rotateSpeed;
        public float maxHealth;
    } 
}
