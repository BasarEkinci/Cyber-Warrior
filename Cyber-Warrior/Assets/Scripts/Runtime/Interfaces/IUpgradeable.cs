namespace Interfaces
{
    public interface IUpgradeable
    {
        int CurrentLevel { get; }
        int MaxLevel { get; }
        int GetLevelPrice(int level);
        void Upgrade();
    }
}
