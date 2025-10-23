using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Data.Context;

public class PsicoContextFactory : IDesignTimeDbContextFactory<PsicoContext>
{
    public PsicoContext CreateDbContext(string[] args)
    {
        var basePath = Directory.GetCurrentDirectory();

        var configuration = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection")
                               ??
                               "Server=localhost,1433;Database=PsicoDB;User Id=sa;Password=Tiago!2025*Strong;TrustServerCertificate=True;Encrypt=False;";

        var optionsBuilder = new DbContextOptionsBuilder<PsicoContext>();
        optionsBuilder.UseSqlServer(connectionString);

        return new PsicoContext(optionsBuilder.Options);
    }
}