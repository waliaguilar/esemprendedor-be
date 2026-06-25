using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EsemprendedorApi.Domain.Entities;

public class SimpleCard
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public int SectionId { get; set; }

    [ForeignKey(nameof(SectionId))]
    public Section Section { get; set; } = null!;

    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(1000)]
    public string Service { get; set; } = string.Empty;

    [Required]
    [MaxLength(500)]
    public string Contact { get; set; } = string.Empty;

    [MaxLength(500)]
    public string? Keywords { get; set; }
}