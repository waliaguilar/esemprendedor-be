using EsemprendedorApi.Domain.Entities;
using EsemprendedorApi.Domain.Interfaces;
using EsemprendedorApi.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EsemprendedorApi.Infrastructure.Repositories;

public class SimpleCardRepository : ISimpleCardRepository
{
    private readonly AppDbContext _context;

    public SimpleCardRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<SimpleCard>> GetAllAsync(int? sectionId = null)
    {
        var query = _context.SimpleCards.AsQueryable();

        if (sectionId.HasValue)
            query = query.Where(sc => sc.SectionId == sectionId.Value);

        return await query.OrderBy(sc => sc.Name).ToListAsync();
    }

    public async Task<SimpleCard?> GetByIdAsync(int id)
    {
        return await _context.SimpleCards.FindAsync(id);
    }

    public async Task<bool> SectionExistsAsync(int sectionId)
    {
        return await _context.Sections.AnyAsync(s => s.Id == sectionId);
    }

    public async Task<SimpleCard> AddAsync(SimpleCard simpleCard)
    {
        _context.SimpleCards.Add(simpleCard);
        await _context.SaveChangesAsync();
        return simpleCard;
    }

    public async Task UpdateAsync(SimpleCard simpleCard)
    {
        _context.SimpleCards.Update(simpleCard);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(SimpleCard simpleCard)
    {
        _context.SimpleCards.Remove(simpleCard);
        await _context.SaveChangesAsync();
    }
}