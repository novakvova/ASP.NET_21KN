using System.ComponentModel.DataAnnotations;

namespace WebATB.Models.Users;

public class UserCreateModel
{
    [Display(Name = "Прізвище")]
    [Required(ErrorMessage = "Вкажіть прізвище")]
    public string LastName { get; set; } = string.Empty;

    [Display(Name="Ім'я")]
    [Required(ErrorMessage = "Вкажіть ім'я")]
    public string Name { get; set; } = string.Empty;

    [Display(Name = "Побатькові")]
    [Required(ErrorMessage = "Вкажіть побатькові")]
    public string MiddleName { get; set; } = string.Empty;

    [Display(Name = "Фото url")]
    [Required(ErrorMessage = "Вкажіть фото url")]
    public IFormFile ? ImageUrl { get; set; }

    [Display(Name = "Телефон")]
    [Required(ErrorMessage = "Вкажіть телефон")]
    public string Phone { get; set; } = string.Empty;
}
