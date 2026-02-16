using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebATB.Data.Entities;

[Table("tblCategories")]
public class CategoryEntity
{
    [Key]
    public int Id { get; set; }
    [Required, StringLength(250)]
    public string Name { get; set; } = null!;
    [Required, StringLength(250)]
    public string Slug { get; set; } = null!;
    [StringLength(255)]
    public string? Image { get; set; }
}
