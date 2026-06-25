using EsemprendedorApi.Application.DTOs;
using EsemprendedorApi.Application.Services.Interfaces;
using EsemprendedorApi.Domain.Entities;
using EsemprendedorApi.Domain.Interfaces;

namespace EsemprendedorApi.Application.Services;

public class CardService : ICardService
{
    private readonly ICardRepository _repository;

    public CardService(ICardRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<CardDto>> GetAllAsync(int? sectionId = null, bool? featured = null)
    {
        var cards = await _repository.GetAllAsync(sectionId, featured);
        return cards.Select(MapToDto);
    }

    public async Task<CardDto?> GetByIdAsync(int id)
    {
        var card = await _repository.GetByIdAsync(id);
        return card is null ? null : MapToDto(card);
    }

    public async Task<(CardDto? result, string? error)> CreateAsync(CreateCardDto dto)
    {
        if (!await _repository.SectionExistsAsync(dto.SectionId))
            return (null, $"Section with id {dto.SectionId} does not exist.");

        var card = new Card
        {
            SectionId = dto.SectionId,
            Icon = dto.Icon,
            Chip = dto.Chip,
            Name = dto.Name,
            Service = dto.Service,
            Contact = dto.Contact,
            Featured = dto.Featured,
            BackgroundImage = dto.BackgroundImage,
            Keywords = dto.Keywords
        };

        var created = await _repository.AddAsync(card);
        return (MapToDto(created), null);
    }

    public async Task<string?> UpdateAsync(int id, UpdateCardDto dto)
    {
        var card = await _repository.GetByIdAsync(id);
        if (card is null)
            return "not_found";

        if (!await _repository.SectionExistsAsync(dto.SectionId))
            return $"Section with id {dto.SectionId} does not exist.";

        card.SectionId = dto.SectionId;
        card.Icon = dto.Icon;
        card.Chip = dto.Chip;
        card.Name = dto.Name;
        card.Service = dto.Service;
        card.Contact = dto.Contact;
        card.Featured = dto.Featured;
        card.BackgroundImage = dto.BackgroundImage;
        card.Keywords = dto.Keywords;

        await _repository.UpdateAsync(card);
        return null;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var card = await _repository.GetByIdAsync(id);
        if (card is null)
            return false;

        await _repository.DeleteAsync(card);
        return true;
    }

    private static CardDto MapToDto(Card card) => new CardDto
    {
        Id = card.Id,
        SectionId = card.SectionId,
        Icon = card.Icon,
        Chip = card.Chip,
        Name = card.Name,
        Service = card.Service,
        Contact = card.Contact,
        Featured = card.Featured,
        BackgroundImage = card.BackgroundImage,
        Keywords = card.Keywords
    };
}