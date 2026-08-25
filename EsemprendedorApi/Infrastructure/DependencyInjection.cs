using EsemprendedorApi.Application.Services;
using EsemprendedorApi.Application.Services.Interfaces;
using EsemprendedorApi.Domain.Interfaces;
using EsemprendedorApi.Infrastructure.Configuration;
using EsemprendedorApi.Infrastructure.Persistence;
using EsemprendedorApi.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EsemprendedorApi.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // ── Database ──────────────────────────────────────────────────────────
        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(connectionString));

        // ── Repositories ──────────────────────────────────────────────────────
        services.AddScoped<ISectionRepository, SectionRepository>();
        services.AddScoped<ICardRepository, CardRepository>();
        services.AddScoped<ISimpleCardRepository, SimpleCardRepository>();

        // ── Services ──────────────────────────────────────────────────────────
        services.AddScoped<ISectionService, SectionService>();
        services.AddScoped<ICardService, CardService>();
        services.AddScoped<ISimpleCardService, SimpleCardService>();

        // ── Vercel Blob Storage ───────────────────────────────────────────────
        services.Configure<VercelBlobSettings>(configuration.GetSection("VercelBlob"));
        services.AddHttpClient<IImageStorageService, VercelBlobStorageService>();

        return services;
    }
}
