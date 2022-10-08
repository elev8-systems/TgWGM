using System.Net;

namespace TelegramBot.Database.Models;

public class Peer
{
    public Guid Id { get; set; }
    
    public Guid UserId { get; set; }
    public User User { get; set; }
    
    public string PeerPublicKey { get; set; }
    public IPAddress IPAddress { get; set; }
}