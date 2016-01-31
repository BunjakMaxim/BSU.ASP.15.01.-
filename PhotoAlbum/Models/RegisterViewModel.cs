using System;
using System.ComponentModel.DataAnnotations;

namespace PhotoAlbum.Models
{
    public class RegisterViewModel
    {
        [ScaffoldColumn(false)]

        public int Id { get; set; }

        [Display(Name = "Enter your login :")]
        [Required]
        public string Login { get; set; }

        [Display(Name = "Enter your e-mail :")]
        [Required]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Поле не может быть пустым")]
        [StringLength(100, ErrorMessage = "The password must contain at least {2} characters", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Enter your password :")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердите пароль :")]
        [Compare("Password", ErrorMessage = "Passwords must match")]
        public string ConfirmPassword { get; set; }
    }
}