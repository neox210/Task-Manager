namespace TaskManager.Models.Settings
{
    public interface IAppSettings
    {
        string this[string key] { get; }
    }
}