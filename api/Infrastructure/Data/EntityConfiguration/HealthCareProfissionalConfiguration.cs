using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.EntityConfiguration;

public class HealthCareProfissionalConfiguration : IEntityTypeConfiguration<HealthCareProfissional>
{
    public void Configure(EntityTypeBuilder<HealthCareProfissional> builder)
    {
        builder.ToTable("HealthCareProfissional");

        builder.HasKey(x => x.Id);

        builder.HasOne(p => p.User)
            .WithMany(c => c.HealthCareProfessionals)
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(p => p.Speciality)
            .WithMany(c => c.HealthCareProfissionals)
            .HasForeignKey(p => p.SpecialityId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(100);

        builder.Property(x => x.CurriculumURL)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(500);

        builder.Property(x => x.UndergraduateURL)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(100);

        builder.Property(x => x.CrpOrCrmURL)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(150);


        builder.Property(x => x.ApprovalStatus)
            .IsRequired()
            .HasColumnType("int");

        builder.Property(x => x.AvailabilityStatus)
            .IsRequired()
            .HasColumnType("int");

    }
}