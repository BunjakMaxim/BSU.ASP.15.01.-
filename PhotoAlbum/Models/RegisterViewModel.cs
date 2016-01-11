using System;
using System.ComponentModel.DataAnnotations;

namespace PhotoAlbum.Models
{
    public class RegisterViewModel
    {
        [ScaffoldColumn(false)]

        public int Id { get; set; }

        [Display(Name = "Введите Ваш логин")]
        [Required(ErrorMessage = "Поле не может быть пустым!")]
        public string Login { get; set; }

        [Display(Name = "Введите Ваш e-mail")]
        [Required(ErrorMessage = "Поле не может быть пустым!")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Поле не может быть пустым")]
        [StringLength(100, ErrorMessage = "Пароль должен содержать не менее {2} символов", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Введите Ваш пароль")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Подтвердите пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердите пароль")]
        [Compare("Password", ErrorMessage = "Пароли должны совпадать")]
        public string ConfirmPassword { get; set; }
    }
}