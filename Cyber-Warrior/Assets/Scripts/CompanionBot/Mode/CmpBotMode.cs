using Data.UnityObjects;
using Enums;
using Managers;
using UnityEngine;

namespace CompanionBot.Mode
{
    public abstract class CmpBotMode : MonoBehaviour
    {
        [Header("Data")]
        public CmpBotDataSO botData;

        [Header("Visuals")]
        public Material eyeMaterial;
        public Color modeColor;

        [Header("Class References")]
        public LevelManager levelManager;
        [Header("Variables")]
        public CmpMode mode;
        public abstract void Initialize();
        public abstract void Execute();
    }
}