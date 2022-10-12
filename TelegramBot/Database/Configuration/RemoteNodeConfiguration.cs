using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TelegramBot.Database.Models;

namespace TelegramBot.Database.Configuration;

public class RemoteNodeConfiguration : IEntityTypeConfiguration<RemoteNode>
{
    public void Configure(EntityTypeBuilder<RemoteNode> entity)
    {
        entity.ToTable("RemoteNodes");

        entity.HasKey(e => e.Id);

        entity.Property(e => e.Host).IsRequired();
        entity.Property(e => e.Port).IsRequired();
        entity.Property(e => e.User).HasMaxLength(256).IsRequired();
        entity.Property(e => e.HostKeys).IsRequired();
        entity.Property(e => e.CountryCode).HasMaxLength(32).IsRequired();
        entity.Property(e => e.DisplayName).HasMaxLength(256).IsRequired();
    }
}