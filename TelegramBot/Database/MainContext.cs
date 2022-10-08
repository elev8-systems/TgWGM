using Microsoft.EntityFrameworkCore;
using TelegramBot.Database.Models;

namespace TelegramBot.Database;

public class MainContext : DbContext
{
    public MainContext(DbContextOptions<MainContext> options) : base(options)
    {
    }
    
    public DbSet<User> Users { get; set; }
    public DbSet<RemoteNode> RemoteNodes { get; set; }
    public DbSet<Peer> Peers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MainContext).Assembly);
    }
}