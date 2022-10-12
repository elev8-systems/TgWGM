using System.Timers;
using Microsoft.Extensions.Logging;
using Renci.SshNet;
using Renci.SshNet.Common;
using SshNet.Security.Cryptography;
using SHA256 = System.Security.Cryptography.SHA256;
using Timer = System.Timers.Timer;

namespace TelegramBot.Services.Remote.Ssh;

public class SshNode
{
    private readonly ILogger _logger;
    private readonly string _interfaceName;
    private readonly SshClient _sshClient;
    private bool _hostKeyVerified;
    private readonly Timer _connectTimer;
    private readonly IDictionary<string, string> _keys;
    private readonly Queue<string> _commandQueue = new();

    public string Host => $"{_sshClient.ConnectionInfo.Host}:{_sshClient.ConnectionInfo.Port}";
    public bool Available => _sshClient.IsConnected && _hostKeyVerified;

    public SshNode(ILogger logger, string host, ushort port, string userName, IDictionary<string, string> keys, string interfaceName)
    {
        _logger = logger;
        _keys = keys;
        _interfaceName = interfaceName;

        _sshClient = new SshClient(host, port, userName, new PrivateKeyFile(@"C:\Users\thewg\.ssh\id_rsa"));
        
        _sshClient.HostKeyReceived += SshClientOnHostKeyReceived;
        _sshClient.ErrorOccurred += SshClientOnErrorOccurred;

        _connectTimer = new Timer
        {
            Interval = 5000,
            AutoReset = true,
            Enabled = false
        };

        _connectTimer.Elapsed += ConnectToHostTimer;
    }

    private void ConnectToHostTimer(object? sender, ElapsedEventArgs e)
    {
        if(_sshClient.IsConnected)
        {
            return;
        }

        try
        {
            _sshClient.Connect();
        }
        catch (Exception exception)
        {
            _logger.LogError(exception:exception, "Error connecting to '{Host}'", Host);
        }
    }

    private void SshClientOnErrorOccurred(object? sender, ExceptionEventArgs e)
    {
        if(!_sshClient.IsConnected)
        {
            return;
        }
        
        _logger.LogError(exception:e.Exception, "Error occured on node '{Host}'", Host);
    }

    private void SshClientOnHostKeyReceived(object? sender, HostKeyEventArgs e)
    {
        var keyType = e.HostKeyName;
        e.CanTrust = false;

        if (!_keys.ContainsKey(e.HostKeyName))
        {
            _logger.LogError(@"No supported fingerprint for '{KeyType}' for node '{Node}'", keyType, Host);
            return;
        }
        
        var expectedKey = _keys[keyType];
        var actualKey = Convert.ToBase64String(e.HostKey);

        if (expectedKey != actualKey)
        {
            _logger.LogError("Host key mismatch for \'{Host}\'.\nExpected: {ExpectedFingerprint}\nGot: {ActualFingerprint}",
                Host,
                expectedKey, actualKey);
            return;
        }

        e.CanTrust = true;
        _hostKeyVerified = e.CanTrust;

        if (!_hostKeyVerified)
        {
            _sshClient.Disconnect();
        }
    }

    public void Start()
    {
        _connectTimer.Enabled = true;
    }
}