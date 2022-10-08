using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TelegramBot.Database.Models;

namespace TelegramBot.Database.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.TelegramId).IsRequired();
        builder.Property(e => e.MaxPeers).IsRequired();

        builder.HasMany(e => e.Peers)
            .WithOne(e => e.User);
    }
}