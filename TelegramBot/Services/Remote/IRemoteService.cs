namespace TelegramBot.Services.Remote;

public interface IRemoteService
{
    void Init();
    void PushConfig();
    void AddPeer();
    void RemovePeer();
    IReadOnlyDictionary<string, bool> Statuses { get; }
}