using EsemprendedorApi.Application.DTOs;
using EsemprendedorApi.Application.Services.Interfaces;
using EsemprendedorApi.Domain.Entities;
using EsemprendedorApi.Domain.Interfaces;

namespace EsemprendedorApi.Application.Services;

public class SimpleCardService : ISimpleCardService
{
    private readonly ISimpleCardRepository _repository;

    public SimpleCardService(ISimpleCardRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<SimpleCardDto>> GetAllAsync(int? sectionId = null)
    {
        var simpleCards = await _repository.GetAllAsync(sectionId);
        return simpleCards.Select(MapToDto);
    }

    public async Task<SimpleCardDto?> GetByIdAsync(int id)
    {
        var simpleCard = await _repository.GetByIdAsync(id);
        return simpleCard is null ? null : MapToDto(simpleCard);
    }

    public async Task<(SimpleCardDto? result, string? error)> CreateAsync(CreateSimpleCardDto dto)
    {
        if (!await _repository.SectionExistsAsync(dto.SectionId))
            return (null, $"Section with id {dto.SectionId} does not exist.");

        var simpleCard = new SimpleCard
        {
            SectionId = dto.SectionId,
            Name = dto.Name,
            Service = dto.Service,
            Contact = dto.Contact,
            Keywords = dto.Keywords
        };

        var created = await _repository.AddAsync(simpleCard);
        return (MapToDto(created), null);
    }

    public async Task<string?> UpdateAsync(int id, UpdateSimpleCardDto dto)
    {
        var simpleCard = await _repository.GetByIdAsync(id);
        if (simpleCard is null)
            return "not_found";

        if (!await _repository.SectionExistsAsync(dto.SectionId))
            return $"Section with id {dto.SectionId} does not exist.";

        simpleCard.SectionId = dto.SectionId;
        simpleCard.Name = dto.Name;
        simpleCard.Service = dto.Service;
        simpleCard.Contact = dto.Contact;
        simpleCard.Keywords = dto.Keywords;

        await _repository.UpdateAsync(simpleCard);
        return null;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var simpleCard = await _repository.GetByIdAsync(id);
        if (simpleCard is null)
            return false;

        await _repository.DeleteAsync(simpleCard);
        return true;
    }

    private static SimpleCardDto MapToDto(SimpleCard simpleCard) => new SimpleCardDto
    {
        Id = simpleCard.Id,
        SectionId = simpleCard.SectionId,
        Name = simpleCard.Name,
        Service = simpleCard.Service,
        Contact = simpleCard.Contact,
        Keywords = simpleCard.Keywords
    };
}