using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.EntityConfiguration;

public class WaitConfiguration : IEntityTypeConfiguration<Wait>
{
    public void Configure(EntityTypeBuilder<Wait> builder)
    {
        builder.ToTable("Wait");

        builder.HasKey(x => x.Id);

        builder.HasOne(p => p.Patient)
            .WithMany(c => c.Waits)
            .HasForeignKey(p => p.PatientId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(p => p.SessionSchedule)
            .WithMany(c => c.Waits)
            .HasForeignKey(p => p.SessionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(x => x.PreferrdeTime)
            .IsRequired()
            .HasColumnType("date");

        builder.HasOne(p => p.Patient)
            .WithMany(c => c.Waits)
            .HasForeignKey(p => p.SessionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(x => x.CreatedaAt)
            .IsRequired()
            .HasColumnType("date");

        builder.Property(x => x.UpdatedAt)
            .IsRequired()
            .HasColumnType("date");

    }
}