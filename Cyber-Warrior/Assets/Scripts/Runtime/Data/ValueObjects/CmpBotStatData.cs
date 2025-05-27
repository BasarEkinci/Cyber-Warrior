namespace Runtime.Data.ValueObjects
{
    [System.Serializable]
    public struct CmpBotStatData
    {
        public CmpCombatData CombatData;
        public CmpVisualData VisualData;
        public CmpHealerData HealerData;
        public int LevelPrice;
    }
}