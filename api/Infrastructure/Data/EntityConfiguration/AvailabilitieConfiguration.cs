using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.EntityConfiguration;

public class AvailabilitieConfiguration : IEntityTypeConfiguration<Availabilitie>
{
    public void Configure(EntityTypeBuilder<Availabilitie> builder)
    {
        builder.ToTable("Availabilitie");

        builder.HasKey(x => x.Id);

        builder.HasOne(p => p.User)
          .WithMany(c => c.Availabilities)
          .HasForeignKey(p => p.BookedByUserId)
          .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(p => p.HealthCareProfissional)
            .WithMany(c => c.Availabilities)
            .HasForeignKey(p => p.ProfissionalId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(p => p.User)
            .WithMany(c => c.Availabilities)
            .HasForeignKey(p => p.BookedByUserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(p => p.User)
            .WithMany(c => c.Availabilities)
            .HasForeignKey(p => p.CreatedBy)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(x => x.AvaliableAt)
            .IsRequired()
            .HasColumnType("date");

        builder.Property(x => x.DurationMinutes)
            .IsRequired()
            .HasColumnType("time");

        builder.Property(x => x.IsBooked)
            .IsRequired()
            .HasColumnType("bool");

        builder.Property(x => x.Source)
            .IsRequired()
            .HasColumnType("int");

        builder.Property(x => x.TypeAvailabilitie)
            .IsRequired()
            .HasColumnType("int");

        builder.Property(x => x.Location)
            .HasColumnType("varchar")
            .HasMaxLength(100);

        builder.Property(x => x.MeetUrl)
            .HasColumnType("varchar")
            .HasMaxLength(100);
    }
}