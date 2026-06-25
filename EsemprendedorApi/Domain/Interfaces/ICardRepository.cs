using EsemprendedorApi.Domain.Entities;

namespace EsemprendedorApi.Domain.Interfaces;

public interface ICardRepository
{
    Task<IEnumerable<Card>> GetAllAsync(int? sectionId = null, bool? featured = null);
    Task<Card?> GetByIdAsync(int id);
    Task<bool> SectionExistsAsync(int sectionId);
    Task<Card> AddAsync(Card card);
    Task UpdateAsync(Card card);
    Task DeleteAsync(Card card);
}