namespace TelegramBot.Services.Remote;

public interface IRemoteService
{
    void Run();
    void PushConfig();
    void AddPeer();
    void RemovePeer();
}