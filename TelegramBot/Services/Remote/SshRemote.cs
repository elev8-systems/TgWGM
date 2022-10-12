using System.Collections.Immutable;
using Microsoft.Extensions.Logging;
using Renci.SshNet;
using TelegramBot.Services.Remote.Ssh;

namespace TelegramBot.Services.Remote;

public class SshRemote : IRemoteService
{
    private readonly ILogger<SshRemote> _logger;

    public SshRemote(ILogger<SshRemote> logger)
    {
        _logger = logger;
    }

    public void Run()
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