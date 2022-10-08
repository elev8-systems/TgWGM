using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TelegramBot.Database.Models;

namespace TelegramBot.Database.Configuration;

public class PeerConfiguration : IEntityTypeConfiguration<Peer>
{
    public void Configure(EntityTypeBuilder<Peer> builder)
    {
        builder.ToTable("Peers");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.PeerPublicKey).IsRequired();
        builder.Property(e => e.IPAddress).IsRequired();
    }
}