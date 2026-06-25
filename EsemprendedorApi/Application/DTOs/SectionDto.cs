namespace EsemprendedorApi.Application.DTOs;

public class SectionDto
{
    public int Id { get; set; }
    public string Slug { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Label { get; set; } = string.Empty;
    public bool BgLight { get; set; }
    public string Keywords { get; set; } = string.Empty;
    public IEnumerable<CardDto> Cards { get; set; } = new List<CardDto>();
    public IEnumerable<SimpleCardDto> SimpleCards { get; set; } = new List<SimpleCardDto>();
}

public class CreateSectionDto
{
    public string Slug { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Label { get; set; } = string.Empty;
    public bool BgLight { get; set; }
    public string Keywords { get; set; } = string.Empty;
}

public class UpdateSectionDto
{
    public string Slug { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Label { get; set; } = string.Empty;
    public bool BgLight { get; set; }
    public string Keywords { get; set; } = string.Empty;
}