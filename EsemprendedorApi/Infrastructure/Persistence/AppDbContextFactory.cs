using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace EsemprendedorApi.Infrastructure.Persistence;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

        // Design-time connection string used only for migrations.
        // At runtime Program.cs uses the real connection string from appsettings.json.
        optionsBuilder.UseNpgsql(
            "Host=localhost;Port=5432;Database=esemprendedor;Username=postgres;Password=postgres;");

        return new AppDbContext(optionsBuilder.Options);
    }
}