using EsemprendedorApi.Domain.Entities;

namespace EsemprendedorApi.Domain.Interfaces;

public interface ISectionRepository
{
    Task<IEnumerable<Section>> GetAllAsync();
    Task<Section?> GetByIdAsync(int id);
    Task<Section?> GetBySlugAsync(string slug);
    Task<bool> SlugExistsAsync(string slug, int? excludeId = null);
    Task<Section> AddAsync(Section section);
    Task UpdateAsync(Section section);
    Task DeleteAsync(Section section);
}