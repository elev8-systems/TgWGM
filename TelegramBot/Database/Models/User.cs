namespace TelegramBot.Database.Models;

public class User
{
    public Guid Id { get; set; }
    public long TelegramId { get; set; }
    public int MaxPeers { get; set; }
    public IList<Peer> Peers { get; set; }
}