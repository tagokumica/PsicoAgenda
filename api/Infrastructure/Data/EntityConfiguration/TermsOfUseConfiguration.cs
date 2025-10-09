using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.EntityConfiguration;

public class TermsOfUseConfiguration : IEntityTypeConfiguration<TermsOfUse>
{
    public void Configure(EntityTypeBuilder<TermsOfUse> builder)
    {
        builder.ToTable("TermsOfUse");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(150);

        builder.Property(x => x.Content)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(500);

        builder.Property(x => x.CreatedAt)
            .HasColumnType("date");

        builder.Property(x => x.Version)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(150);
    }
}