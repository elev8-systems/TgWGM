using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
using System.Text.Json.Nodes;

namespace TelegramBot.Database.Models;

public class RemoteNode
{
    public Guid Id { get; set; }
    public string Host { get; set; }
    public ushort Port { get; set; }
    public string User { get; set; }
    
    [Column(TypeName = "jsonb")]
    public string HostKeys { get; set; }
    public string CountryCode { get; set; }
    public string DisplayName { get; set; }
    public string InterfaceName { get; set; }
}