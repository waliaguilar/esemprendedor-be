using EsemprendedorApi.Application.DTOs;

namespace EsemprendedorApi.Application.Services.Interfaces;

public interface ISectionService
{
    Task<IEnumerable<SectionDto>> GetAllAsync();
    Task<SectionDto?> GetByIdAsync(int id);
    Task<SectionDto?> GetBySlugAsync(string slug);
    Task<(SectionDto? result, string? error)> CreateAsync(CreateSectionDto dto);
    Task<string?> UpdateAsync(int id, UpdateSectionDto dto);
    Task<bool> DeleteAsync(int id);
}