using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.EntityConfiguration;

public class TermsAcceptanceConfiguration : IEntityTypeConfiguration<TermsAcceptance>
{
    public void Configure(EntityTypeBuilder<TermsAcceptance> builder)
    {
        builder.ToTable("TermsAcceptance");

        builder.HasKey(x => x.Id);

        builder.HasOne(p => p.User)
            .WithMany(c => c.TermsAcceptance)
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(p => p.TermsOfUse)
            .WithMany(c => c.TermsAcceptance)
            .HasForeignKey(p => p.TermsOfUseId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(x => x.Content)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(1000);


        builder.Property(x => x.CreatedAt)
            .IsRequired()
            .HasColumnType("date");

        builder.Property(x => x.IsAgreed)
            .IsRequired()
            .HasColumnType("bool");

    }
}