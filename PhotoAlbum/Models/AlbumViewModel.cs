using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PhotoAlbum.Models
{
    public class AlbumViewModel
    {
        [Required(ErrorMessage = "Поле не может быть пустым!"), Display(Name = "Название альбома")]
        public string Name { get; set; }
    }
}