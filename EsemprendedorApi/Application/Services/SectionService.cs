using EsemprendedorApi.Application.DTOs;
using EsemprendedorApi.Application.Services.Interfaces;
using EsemprendedorApi.Domain.Entities;
using EsemprendedorApi.Domain.Interfaces;

namespace EsemprendedorApi.Application.Services;

public class SectionService : ISectionService
{
    private readonly ISectionRepository _repository;

    public SectionService(ISectionRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<SectionDto>> GetAllAsync()
    {
        var sections = await _repository.GetAllAsync();
        return sections.Select(MapToDto);
    }

    public async Task<SectionDto?> GetByIdAsync(int id)
    {
        var section = await _repository.GetByIdAsync(id);
        return section is null ? null : MapToDto(section);
    }

    public async Task<SectionDto?> GetBySlugAsync(string slug)
    {
        var section = await _repository.GetBySlugAsync(slug);
        return section is null ? null : MapToDto(section);
    }

    public async Task<(SectionDto? result, string? error)> CreateAsync(CreateSectionDto dto)
    {
        if (await _repository.SlugExistsAsync(dto.Slug))
            return (null, $"A section with slug '{dto.Slug}' already exists.");

        var section = new Section
        {
            Slug = dto.Slug,
            Title = dto.Title,
            Label = dto.Label,
            BgLight = dto.BgLight,
            Keywords = dto.Keywords
        };

        var created = await _repository.AddAsync(section);
        return (MapToDto(created), null);
    }

    public async Task<string?> UpdateAsync(int id, UpdateSectionDto dto)
    {
        var section = await _repository.GetByIdAsync(id);
        if (section is null)
            return "not_found";

        if (await _repository.SlugExistsAsync(dto.Slug, excludeId: id))
            return $"A section with slug '{dto.Slug}' already exists.";

        section.Slug = dto.Slug;
        section.Title = dto.Title;
        section.Label = dto.Label;
        section.BgLight = dto.BgLight;
        section.Keywords = dto.Keywords;

        await _repository.UpdateAsync(section);
        return null;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var section = await _repository.GetByIdAsync(id);
        if (section is null)
            return false;

        await _repository.DeleteAsync(section);
        return true;
    }

    private static SectionDto MapToDto(Section section) => new SectionDto
    {
        Id = section.Id,
        Slug = section.Slug,
        Title = section.Title,
        Label = section.Label,
        BgLight = section.BgLight,
        Keywords = section.Keywords,
        Cards = section.Cards.Select(c => new CardDto
        {
            Id = c.Id,
            SectionId = c.SectionId,
            Icon = c.Icon,
            Chip = c.Chip,
            Name = c.Name,
            Service = c.Service,
            Contact = c.Contact,
            Featured = c.Featured,
            BackgroundImage = c.BackgroundImage,
            Keywords = c.Keywords
        }),
        SimpleCards = section.SimpleCards.Select(sc => new SimpleCardDto
        {
            Id = sc.Id,
            SectionId = sc.SectionId,
            Name = sc.Name,
            Service = sc.Service,
            Contact = sc.Contact,
            Keywords = sc.Keywords
        })
    };
}