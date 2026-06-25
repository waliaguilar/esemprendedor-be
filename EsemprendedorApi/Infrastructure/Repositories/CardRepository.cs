using EsemprendedorApi.Domain.Entities;
using EsemprendedorApi.Domain.Interfaces;
using EsemprendedorApi.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EsemprendedorApi.Infrastructure.Repositories;

public class CardRepository : ICardRepository
{
    private readonly AppDbContext _context;

    public CardRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Card>> GetAllAsync(int? sectionId = null, bool? featured = null)
    {
        var query = _context.Cards.AsQueryable();

        if (sectionId.HasValue)
            query = query.Where(c => c.SectionId == sectionId.Value);

        if (featured.HasValue)
            query = query.Where(c => c.Featured == featured.Value);

        return await query.OrderBy(c => c.Name).ToListAsync();
    }

    public async Task<Card?> GetByIdAsync(int id)
    {
        return await _context.Cards.FindAsync(id);
    }

    public async Task<bool> SectionExistsAsync(int sectionId)
    {
        return await _context.Sections.AnyAsync(s => s.Id == sectionId);
    }

    public async Task<Card> AddAsync(Card card)
    {
        _context.Cards.Add(card);
        await _context.SaveChangesAsync();
        return card;
    }

    public async Task UpdateAsync(Card card)
    {
        _context.Cards.Update(card);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Card card)
    {
        _context.Cards.Remove(card);
        await _context.SaveChangesAsync();
    }
}