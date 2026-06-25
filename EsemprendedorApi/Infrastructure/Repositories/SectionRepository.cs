using EsemprendedorApi.Domain.Entities;
using EsemprendedorApi.Domain.Interfaces;
using EsemprendedorApi.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EsemprendedorApi.Infrastructure.Repositories;

public class SectionRepository : ISectionRepository
{
    private readonly AppDbContext _context;

    public SectionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Section>> GetAllAsync()
    {
        return await _context.Sections
            .Include(s => s.Cards)
            .Include(s => s.SimpleCards)
            .OrderBy(s => s.Label)
            .ToListAsync();
    }

    public async Task<Section?> GetByIdAsync(int id)
    {
        return await _context.Sections
            .Include(s => s.Cards)
            .Include(s => s.SimpleCards)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<Section?> GetBySlugAsync(string slug)
    {
        return await _context.Sections
            .Include(s => s.Cards)
            .Include(s => s.SimpleCards)
            .FirstOrDefaultAsync(s => s.Slug == slug);
    }

    public async Task<bool> SlugExistsAsync(string slug, int? excludeId = null)
    {
        return await _context.Sections
            .AnyAsync(s => s.Slug == slug && (excludeId == null || s.Id != excludeId));
    }

    public async Task<Section> AddAsync(Section section)
    {
        _context.Sections.Add(section);
        await _context.SaveChangesAsync();
        return section;
    }

    public async Task UpdateAsync(Section section)
    {
        _context.Sections.Update(section);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Section section)
    {
        _context.Sections.Remove(section);
        await _context.SaveChangesAsync();
    }
}