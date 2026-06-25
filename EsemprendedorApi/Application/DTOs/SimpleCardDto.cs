namespace EsemprendedorApi.Application.DTOs;

public class SimpleCardDto
{
    public int Id { get; set; }
    public int SectionId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Service { get; set; } = string.Empty;
    public string Contact { get; set; } = string.Empty;
    public string? Keywords { get; set; }
}

public class CreateSimpleCardDto
{
    public int SectionId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Service { get; set; } = string.Empty;
    public string Contact { get; set; } = string.Empty;
    public string? Keywords { get; set; }
}

public class UpdateSimpleCardDto
{
    public int SectionId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Service { get; set; } = string.Empty;
    public string Contact { get; set; } = string.Empty;
    public string? Keywords { get; set; }
}