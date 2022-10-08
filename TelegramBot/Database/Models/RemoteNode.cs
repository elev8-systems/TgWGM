using System.Net;

namespace TelegramBot.Database.Models;

public class RemoteNode
{
    public Guid Id { get; set; }
    public IPAddress Host { get; set; }
    public ushort Port { get; set; }
    public string User { get; set; }
    public string HostKey { get; set; }
    public string CountryCode { get; set; }
    public string DisplayName { get; set; }
}