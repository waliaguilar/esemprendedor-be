using EsemprendedorApi.Application.DTOs;

namespace EsemprendedorApi.Application.Services.Interfaces;

public interface ICardService
{
    Task<IEnumerable<CardDto>> GetAllAsync(int? sectionId = null, bool? featured = null);
    Task<CardDto?> GetByIdAsync(int id);
    Task<(CardDto? result, string? error)> CreateAsync(CreateCardDto dto);
    Task<string?> UpdateAsync(int id, UpdateCardDto dto);
    Task<bool> DeleteAsync(int id);
}