using Runtime.Data.UnityObjects.ObjectData;
using Runtime.Enums;
using Runtime.Managers;
using UnityEngine;

namespace Runtime.CompanionBot.Mode
{
    public abstract class CmpBotMode : MonoBehaviour
    {
        public abstract GameState ValidGameState { get; }
        public abstract Transform TargetObject { get; set; }
        public abstract Transform FollowPosition { get; set; }
        [Header("Mode Info")]
        public CmpMode mode;
        
        [Header("Data")]
        public CmpBotDataSO botData;

        [Header("Visuals")]
        public Material eyeMaterial;
        public Color modeColor;

        [Header("Class References")]
        public LevelManager levelManager;
        public BotAnchorPoints anchorPoints;
        public abstract void Initialize();
        public abstract void Execute();
        public abstract void ExitState();
        public abstract void RotateBehaviour(Transform currentTransform);
        public abstract void Move(Transform currentTransform, float deltaTime);
    }
}