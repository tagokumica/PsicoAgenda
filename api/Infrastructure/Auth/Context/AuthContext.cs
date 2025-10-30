using Infrastructure.Auth.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Auth.Context
{
    public class AuthContext : IdentityDbContext<UserApplication, RoleApplication, Guid>
    {
        public AuthContext(DbContextOptions<AuthContext> options) : base(options) { }

        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<RefreshToken>(cfg =>
            {
                cfg.HasKey(x => x.Id);
                cfg.Property(x => x.Token).IsRequired();
                cfg.HasIndex(x => x.Token).IsUnique();
                cfg.Property(x => x.UserId).IsRequired();
            });
        }
    }
}

