using Data.UnityObjects;
using Enums;
using Managers;
using UnityEngine;

namespace Runtime.CompanionBot.Mode
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
        public CmpMode mode;

        [Header("Values")] 
        public Transform targetObject;
        public Transform followPosition;
        public abstract void Initialize();
        public abstract void Execute();
        public abstract void RotateBehaviour(Transform currentTransform);
        public abstract void MoveBehaviourFixed(Transform currentTransform);
    }
}