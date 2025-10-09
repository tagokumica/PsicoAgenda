using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.EntityConfiguration;

public class SessionScheduleConfiguration : IEntityTypeConfiguration<SessionSchedule>
{
    public void Configure(EntityTypeBuilder<SessionSchedule> builder)
    {
        builder.ToTable("SessionSchedule");

        builder.HasKey(x => x.Id);

        builder.HasOne(p => p.Patient)
            .WithMany(c => c.SessionSchedules)
            .HasForeignKey(p => p.PatientId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(p => p.User)
            .WithMany(c => c.SessionSchedules)
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(p => p.HealthCareProfissional)
            .WithMany(c => c.SessionSchedules)
            .HasForeignKey(p => p.ProfessionalId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(x => x.AvaliableAt)
            .IsRequired()
            .HasColumnType("date");

        builder.Property(x => x.Status)
            .IsRequired()
            .HasColumnType("int");

        builder.Property(x => x.DurationMinute)
            .IsRequired()
            .HasColumnType("time");
    }
}