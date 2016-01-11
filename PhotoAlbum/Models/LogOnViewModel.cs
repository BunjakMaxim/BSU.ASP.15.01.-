using System.ComponentModel.DataAnnotations;

namespace PhotoAlbum.Models
{
    public class LogOnViewModel
    {
        [Required(ErrorMessage = "Поле не может быть пустым!")]
        [Display(Name = "Введите логин")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Поле не может быть пустым!")]
        [DataType(DataType.Password)]
        [Display(Name = "Введите пароль")]
        public string Password { get; set; }

        [Display(Name = "Запомнить пароль?")]
        public bool RememberMe { get; set; }
    }
}
