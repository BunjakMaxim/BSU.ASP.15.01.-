using System;
using System.ComponentModel.DataAnnotations;

namespace PhotoAlbum.Models
{
    public class UserViewModel
    {
        [Display(Name = "E-mail пользователя")]
        public string Email { get; set; }

        [Display(Name = "Дата регистрации пользователя")]
        public DateTime CreationDate { get; set; }

        [Display(Name = "Роль пользователя в системе")]
        public string Role { get; set; }
    }
}