namespace Runtime.Interfaces
{
    public interface ISaveable
    {
        string SaveId { get; }
        void Save();
        void Load();
    }
}