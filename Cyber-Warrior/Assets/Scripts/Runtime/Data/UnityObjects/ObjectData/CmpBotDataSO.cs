﻿using System.Collections.Generic;
using Runtime.Data.ValueObjects;
using UnityEngine;

namespace Runtime.Data.UnityObjects.ObjectData
{
    [CreateAssetMenu(fileName = "Companion Bot", menuName = "Scriptable Objects/Companion Bot", order = 0)]
    public class CmpBotDataSO : ScriptableObject
    {
        public int MaxLevel => statDataList.Count - 1;
        public CmpMovementData movementData;
        [Header("Data List")]
        public List<CmpBotStatData> statDataList;
    }
}