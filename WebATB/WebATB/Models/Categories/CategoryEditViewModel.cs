using System.ComponentModel.DataAnnotations;

namespace WebATB.Models.Categories;

public class CategoryEditViewModel
{
    public int Id { get; set; } //Вказуємо Id - яку будемо редагуванти
    [Display(Name = "Вкажіть назву категорії")]
    [Required(ErrorMessage = "Вкажіть назву категорії")]
    public string Name { get; set; } = null!;
    [Display(Name = "Вкажіть Slug")]
    [Required(ErrorMessage = "Вкажіть Slug")]
    public string Slug { get; set; } = null!;
    public string ? OldImage { get; set; }
    [Display(Name = "Оберіть фото категорії")]
    [Required(ErrorMessage = "Вкажіть фото для категорії")]
    public IFormFile? FileImage { get; set; }
}
