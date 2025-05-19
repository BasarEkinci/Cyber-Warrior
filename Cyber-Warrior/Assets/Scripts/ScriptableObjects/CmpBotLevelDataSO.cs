using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "CmpBotLevelData", menuName = "Scriptable Objects/CmpBotLevelData", order = 0)]
    public class CmpBotLevelDataSO : ScriptableObject
    {
        public List<CmpBotLevelData> levelDataList;
        public int MaxLevel => levelDataList.Count - 1;
    }

    [System.Serializable]
    public struct CmpBotLevelData
    {
        public int price;
    }
}
