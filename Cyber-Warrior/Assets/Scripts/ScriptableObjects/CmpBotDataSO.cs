using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Companion Bot", menuName = "Scriptable Objects/Companion Bot", order = 0)]
    public class CmpBotDataSO : ScriptableObject
    {
        public int MaxLevel => statDataList.Count - 1;
        public List<CmpBotStatData> statDataList;
    }

    [System.Serializable]
    public struct CmpBotStatData
    {
        public CmpCombatData CombatData;
        public CmpVisualData VisualData;
        public int LevelPrice;
    }

    [System.Serializable]
    public struct CmpCombatData
    {
        public float AttackCooldown;
        public float Damage;
        public float MoveSpeed;
        public float RotationSpeed;
        public Vector3 FollowOffset;
        public Vector3 AttackOffset;
        public LayerMask EnemyLayer;
    }

    [System.Serializable]
    public struct CmpVisualData
    {
        public Material EyeMaterial;
        public GameObject Mesh; //This value will change based on the level
    }
}