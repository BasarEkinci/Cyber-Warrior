namespace Runtime.Interfaces
{
    public interface IUpgradeable
    {
        int CurrentLevel { get; set; }
        int MaxLevel { get; set; }
        int GetLevelPrice(int level);
        void Upgrade();
    }
}
