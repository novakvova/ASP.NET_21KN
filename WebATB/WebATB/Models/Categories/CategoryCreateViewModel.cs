namespace WebATB.Models.Categories;

public class CategoryCreateViewModel
{
    public string Name { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public IFormFile? FileImage { get; set; }
}
