using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.EntityConfiguration;

public class SessionNoteConfiguration : IEntityTypeConfiguration<SessionNote>
{
    public void Configure(EntityTypeBuilder<SessionNote> builder)
    {
        builder.ToTable("SessionNote");

        builder.HasKey(x => x.Id);

        builder.HasOne(p => p.SessionSchedule)
            .WithMany(c => c.SessionNotes)
            .HasForeignKey(p => p.SessionScheduleId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(p => p.User)
            .WithMany(c => c.SessionNotes)
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(p => p.HealthCareProfissional)
            .WithMany(c => c.SessionNotes)
            .HasForeignKey(p => p.ProfessionalId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(x => x.Content)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(500);

        builder.Property(x => x.Tags)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(150);

        builder.Property(x => x.Insight)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(100);
    }
}