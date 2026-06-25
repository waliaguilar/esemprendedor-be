using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EsemprendedorApi.Domain.Entities;

public class Section
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Slug { get; set; } = string.Empty;

    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string Label { get; set; } = string.Empty;

    public bool BgLight { get; set; }

    [MaxLength(500)]
    public string Keywords { get; set; } = string.Empty;

    public ICollection<Card> Cards { get; set; } = new List<Card>();
    public ICollection<SimpleCard> SimpleCards { get; set; } = new List<SimpleCard>();
}