using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.EntityConfiguration;

public class AddressConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.ToTable("Address");

        builder.HasKey(x => x.Id);                  

        builder.Property(x => x.Street)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(120);

        builder.Property(x => x.Number)
            .IsRequired()
            .HasColumnType("int");

        builder.Property(x => x.Complement)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(200);

        builder.Property(x => x.City)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(100);

        builder.Property(x => x.ZipCode)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(9);

        builder.Property(x => x.State)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(50);
    }
}