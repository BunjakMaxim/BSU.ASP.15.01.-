using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAlbum.Business.DTO
{
    public class UserDTO
    {
        public string Login { get; set; }
        public string Email { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsBlocked { get; set; }
        public string Role { get; set; }
    }
}
