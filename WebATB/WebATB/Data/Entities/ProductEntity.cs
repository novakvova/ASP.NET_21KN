using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebATB.Data.Entities;

[Table("tblProducts")]
public class ProductEntity
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("Category")]
    public int CategoryId { get; set; }
    public CategoryEntity Category { get; set; } = null!;

    [Required, StringLength(500)]
    public string Name { get; set; } = String.Empty;

    [Required, StringLength(500)]
    public string Slug { get; set; } = null!;

    public decimal Price { get; set; }
    public decimal? Sale { get; set; }

    [StringLength(10000)]
    public string? Description { get; set; }

    [StringLength(5000)]
    public string? GeneralInfo { get; set; }

    [Required, StringLength(250)]
    public string Image { get; set; } = null!;
}
