using System;
using Enums;
using Runtime.Data.ValueObjects;
using UnityEngine;

namespace Runtime.CompanionBot.Mode
{
    public class AttackerBotMode : CmpBotMode
    {
        [SerializeField] private TransformEventChannel onTargetChange;
        private CmpCombatData _botData;
        public override GameState ValidGameState => GameState.Action;
        public override Transform TargetObject { get; set; }
        public override Transform FollowPosition { get; set; }
        private CmpBotVFXPlayer _vfxPlayer;
        private float _timer;

        public override void Initialize()
        {
            _vfxPlayer = transform.parent.GetComponentInChildren<CmpBotVFXPlayer>();
            TargetObject = anchorPoints.GetInitialTargetObject();
            FollowPosition = anchorPoints.GetAnchorPoint(mode);
            eyeMaterial.color = modeColor;
            _botData = GetDataAtCurrentLevel().combatData;
        }
        public override void Execute()
        {
            _timer += Time.deltaTime;
            if (_timer >= _botData.attackCooldown)
            {
                _vfxPlayer.PlayVFX();
                _timer = 0f;
            }
        }

        public override void RotateBehaviour(Transform currentTransform)
        {
            currentTransform.LookAt(TargetObject);
        }

        public override void MoveBehaviourFixed(Transform currentTransform)
        {
            Vector3 desiredPosition = FollowPosition.position;
            currentTransform.position = Vector3.Lerp(currentTransform.position, desiredPosition, botData.movementData.moveSpeed * Time.fixedDeltaTime);
        }
        public override CmpBotStatData GetDataAtCurrentLevel()
        {
            return botData.statDataList[levelManager.CurrentLevel];
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _botData.range);
        }
#endif
    }
}