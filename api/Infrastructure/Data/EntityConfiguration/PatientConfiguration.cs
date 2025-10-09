using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.EntityConfiguration;

public class PatientConfiguration : IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
        builder.ToTable("Patient");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(150);

        builder.Property(x => x.Email)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(100);

        builder.Property(x => x.BirthDate)
            .HasColumnType("date");

        builder.Property(x => x.Cpf)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(11);

        builder.Property(x => x.Notes)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(500);

        builder.Property(x => x.Gender)
            .IsRequired()
            .HasColumnType("int");

        builder.Property(x => x.EmergencyContract)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(150);

    }
}