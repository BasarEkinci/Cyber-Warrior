using Enums;
using UnityEngine;

namespace Runtime.CompanionBot
{
    public class BotAnchorPoints : MonoBehaviour
    {
        [Header("Anchor Points")]
        public Transform healerAnchorPoint;
        public Transform attackerAnchorPoint;
        public Transform baseAnchorPoint;
        
        [Header("Targets")]
        public Transform targetObject;

        public Transform GetAnchorPoint(CmpMode mode)
        {
            return mode switch
            {
                CmpMode.Base => baseAnchorPoint,
                CmpMode.Healer => healerAnchorPoint,
                CmpMode.Attacker => attackerAnchorPoint,
                _ => healerAnchorPoint
            };
        }

        public Transform GetInitialTargetObject()
        {
            return targetObject;
        }
    }
}
