using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.EntityConfiguration;

public class LocationConfiguration : IEntityTypeConfiguration<Location>
{
    public void Configure(EntityTypeBuilder<Location> builder)
    {
        builder.ToTable("Location");

        builder.HasKey(x => x.Id);

        builder.HasOne(p => p.Address)
            .WithMany(c => c.Locations)
            .HasForeignKey(p => p.AddressId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(100);
    }
}