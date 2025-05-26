using Data.UnityObjects;
using Data.ValueObjects;
using UnityEngine;

namespace CompanionBot.Mode
{
    public class AttackerBotMode : CmpBotMode
    {
        [SerializeField] private CmpBotDataSO data;
        [SerializeField] private TransformEventChannel onTargetChange;
        private CmpCombatData _botData;
        public override void Initialize()
        {
            eyeMaterial.color = modeColor;
            _botData = data.statDataList[levelManager.CurrentLevel].CombatData;
        }
        public override void Execute()
        {
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _botData.Range);
        }
#endif
    }
}