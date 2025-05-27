namespace Runtime.Data.ValueObjects
{
    [System.Serializable]
    public struct CmpBotStatData
    {
        public CmpCombatData combatData;
        public CmpVisualData visualData;
        public CmpHealerData healerData;
        public int levelPrice;
    }
}