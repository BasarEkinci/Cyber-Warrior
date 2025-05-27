using Data.UnityObjects;
using Enums;
using Managers;
using UnityEngine;

namespace Runtime.CompanionBot.Mode
{
    public abstract class CmpBotMode : MonoBehaviour
    {
        public abstract GameState ValidGameState { get; }
        [Header("Data")]
        public CmpBotDataSO botData;
        public CmpMode mode;

        [Header("Visuals")]
        public Material eyeMaterial;
        public Color modeColor;

        [Header("Class References")]
        public LevelManager levelManager;

        [Header("Values")] 
        public Transform targetObject;
        public Transform followPosition;
        public abstract void Initialize();
        public abstract void Execute();
        public abstract void RotateBehaviour(Transform currentTransform);
        public abstract void MoveBehaviourFixed(Transform currentTransform);
    }
}