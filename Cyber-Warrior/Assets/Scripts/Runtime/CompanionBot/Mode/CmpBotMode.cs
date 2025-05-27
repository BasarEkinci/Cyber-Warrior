using Data.UnityObjects;
using Enums;
using Managers;
using Runtime.Data.ValueObjects;
using UnityEngine;

namespace Runtime.CompanionBot.Mode
{
    public abstract class CmpBotMode : MonoBehaviour
    {
        [Header("Mode Info")]
        public CmpMode mode;
        public abstract GameState ValidGameState { get; }
        [Header("Data")]
        public CmpBotDataSO botData;

        [Header("Visuals")]
        public Material eyeMaterial;
        public Color modeColor;

        [Header("Class References")]
        public LevelManager levelManager;

        [Header("Values")] 
        public Transform targetObject;
        public Transform followPosition;
        public abstract CmpBotStatData GetDataAtCurrentLevel();
        public abstract void Initialize();
        public abstract void Execute();
        public abstract void RotateBehaviour(Transform currentTransform);
        public abstract void MoveBehaviourFixed(Transform currentTransform);
    }
}