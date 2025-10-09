using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.EntityConfiguration;

public class ConsentConfiguration : IEntityTypeConfiguration<Consent>
{
    public void Configure(EntityTypeBuilder<Consent> builder)
    {
        builder.ToTable("Consent");

        builder.HasKey(x => x.Id);

        builder.HasOne(p => p.Patient)
            .WithMany(c => c.Consents)
            .HasForeignKey(p => p.PatientId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(x => x.ConsentType)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(100); 

        builder.Property(x => x.GivenAt)
            .IsRequired()
            .HasColumnType("date");

        builder.Property(x => x.Version)
            .IsRequired()
            .HasColumnType("string")
            .HasMaxLength(100);

    }
}