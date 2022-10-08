using System.Collections.Immutable;
using Renci.SshNet;

namespace TelegramBot.Services.Remote;

public class SshRemote : IRemoteService
{
    private readonly Dictionary<string, ConnectionInfo> _connectionInfos = new();
    private readonly Dictionary<string, SshClient> _clients = new();

    public IReadOnlyDictionary<string, bool> Statuses =>
        _clients.ToImmutableDictionary(pair => pair.Key, pair => pair.Value.IsConnected);

    public void Init()
    {
        
    }

    public void PushConfig()
    {
        
    }

    public void AddPeer()
    {
        
    }

    public void RemovePeer()
    {
        
    }
}