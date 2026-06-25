using EsemprendedorApi.Domain.Entities;

namespace EsemprendedorApi.Domain.Interfaces;

public interface ISimpleCardRepository
{
    Task<IEnumerable<SimpleCard>> GetAllAsync(int? sectionId = null);
    Task<SimpleCard?> GetByIdAsync(int id);
    Task<bool> SectionExistsAsync(int sectionId);
    Task<SimpleCard> AddAsync(SimpleCard simpleCard);
    Task UpdateAsync(SimpleCard simpleCard);
    Task DeleteAsync(SimpleCard simpleCard);
}