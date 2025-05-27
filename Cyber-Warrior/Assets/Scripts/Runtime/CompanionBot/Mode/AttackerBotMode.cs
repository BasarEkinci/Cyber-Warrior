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

        public override void Initialize()
        {
            eyeMaterial.color = modeColor;
            _botData = GetDataAtCurrentLevel().CombatData;
        }
        public override void Execute()
        {
        }

        public override void RotateBehaviour(Transform currentTransform)
        {
            currentTransform.LookAt(targetObject);
        }

        public override void MoveBehaviourFixed(Transform currentTransform)
        {
            Vector3 desiredPosition = followPosition.position;
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