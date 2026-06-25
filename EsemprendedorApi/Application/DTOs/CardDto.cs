namespace EsemprendedorApi.Application.DTOs;

public class CardDto
{
    public int Id { get; set; }
    public int SectionId { get; set; }
    public string Icon { get; set; } = string.Empty;
    public string Chip { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Service { get; set; } = string.Empty;
    public string Contact { get; set; } = string.Empty;
    public bool Featured { get; set; }
    public string? BackgroundImage { get; set; }
    public string? Keywords { get; set; }
}

public class CreateCardDto
{
    public int SectionId { get; set; }
    public string Icon { get; set; } = string.Empty;
    public string Chip { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Service { get; set; } = string.Empty;
    public string Contact { get; set; } = string.Empty;
    public bool Featured { get; set; }
    public string? BackgroundImage { get; set; }
    public string? Keywords { get; set; }
}

public class UpdateCardDto
{
    public int SectionId { get; set; }
    public string Icon { get; set; } = string.Empty;
    public string Chip { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Service { get; set; } = string.Empty;
    public string Contact { get; set; } = string.Empty;
    public bool Featured { get; set; }
    public string? BackgroundImage { get; set; }
    public string? Keywords { get; set; }
}