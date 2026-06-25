using EsemprendedorApi.Application.DTOs;

namespace EsemprendedorApi.Application.Services.Interfaces;

public interface ISimpleCardService
{
    Task<IEnumerable<SimpleCardDto>> GetAllAsync(int? sectionId = null);
    Task<SimpleCardDto?> GetByIdAsync(int id);
    Task<(SimpleCardDto? result, string? error)> CreateAsync(CreateSimpleCardDto dto);
    Task<string?> UpdateAsync(int id, UpdateSimpleCardDto dto);
    Task<bool> DeleteAsync(int id);
}