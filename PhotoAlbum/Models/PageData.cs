using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoAlbum.Models
{
    public class PageData<T>
    {
        private int Size;
        public IEnumerable<T> Content { get; set; }
        public int NumberPage { get; set; }
        public int CountPage { get; set; }
        
        public PageData(IEnumerable<T> coutent, int numPage, int size = 12)
        {
            Size = size;
            NumberPage = numPage;
            CountPage = coutent.Count()/(Size + 1) + 1;
            Content = coutent.Skip((numPage - 1) * Size).Take(Size);
        }
    }
}