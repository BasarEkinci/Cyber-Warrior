using Enums;
using Runtime.Data.ValueObjects;
using UnityEngine;

namespace Runtime.CompanionBot.Mode
{
    public class AttackerBotMode : CmpBotMode
    {
        [SerializeField] private TransformEventChannel onTargetChange;
        private CmpCombatData _combatData;
        public override GameState ValidGameState => GameState.Action;
        public override Transform TargetObject { get; set; }
        public override Transform FollowPosition { get; set; }
        private CmpBotVFXPlayer _vfxPlayer;
        private float _timer;
        private Transform _parent;

        private void Awake()
        {
            _parent = transform.parent;
        }
        public override void Initialize()
        {
            if (_vfxPlayer == null)
                _vfxPlayer = transform.parent.GetComponentInChildren<CmpBotVFXPlayer>(true);

            if (TargetObject == null)
                TargetObject = anchorPoints.GetInitialTargetObject();

            if (FollowPosition == null)
                FollowPosition = anchorPoints.GetAnchorPoint(mode);

            if (eyeMaterial != null)
                eyeMaterial.color = modeColor;

            _combatData = GetDataAtCurrentLevel().combatData;
        }
        public override void Execute()
        {
            _timer += Time.deltaTime;
            if (_timer >= _combatData.attackCooldown)
            {
                _vfxPlayer.PlayVFX();
                _timer = 0f;
            }
        }

        public override void RotateBehaviour(Transform currentTransform)
        {
            currentTransform.LookAt(TargetObject);
        }

        public override void Move(Transform currentTransform,float deltaTime)
        {
            Vector3 desiredPosition = FollowPosition.position;
            currentTransform.position = Vector3.Lerp(currentTransform.position, desiredPosition, botData.movementData.moveSpeed * deltaTime);
        }
        public override CmpBotStatData GetDataAtCurrentLevel()
        {
            return botData.statDataList[levelManager.CurrentLevel];
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _combatData.range);
        }
#endif
    }
}