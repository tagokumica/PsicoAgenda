using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.EntityConfiguration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User");

        builder.HasKey(x => x.Id);

        builder.HasOne(p => p.Address)
            .WithMany(c => c.Users)
            .HasForeignKey(p => p.AddressId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(100);

        builder.Property(x => x.Email)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(50);

        builder.Property(x => x.Gender)
            .IsRequired()
            .HasColumnType("int");

        builder.Property(x => x.Avatar)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(500);
    }
}