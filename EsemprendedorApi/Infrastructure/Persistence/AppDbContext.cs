using EsemprendedorApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EsemprendedorApi.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Section> Sections { get; set; }
    public DbSet<Card> Cards { get; set; }
    public DbSet<SimpleCard> SimpleCards { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Section>(entity =>
        {
            entity.HasIndex(s => s.Slug).IsUnique();

            entity.HasMany(s => s.Cards)
                  .WithOne(c => c.Section)
                  .HasForeignKey(c => c.SectionId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(s => s.SimpleCards)
                  .WithOne(sc => sc.Section)
                  .HasForeignKey(sc => sc.SectionId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Card>(entity =>
        {
            entity.Property(c => c.Featured).HasDefaultValue(false);
        });

        modelBuilder.Entity<SimpleCard>(entity =>
        {
            entity.ToTable("SimpleCards");
        });
    }
}