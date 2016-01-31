using System.ComponentModel.DataAnnotations;

namespace PhotoAlbum.Models
{
    public class LogOnViewModel
    {
        [Required]
        [Display(Name = "Enter login :")]
        public string Login { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Enter password :")]
        public string Password { get; set; }

        [Display(Name = "Remember password?")]
        public bool RememberMe { get; set; }
    }
}
