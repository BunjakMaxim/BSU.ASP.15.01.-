using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoAlbum.Models
{
    public class RatingViewModel
    {
        public double Rating { private get; set; }
        public int NewRating { get; set; }
        public bool IsMY { get; set; }
        public int IdPhoto { get; set; }
        public string RatingString
        {
            get
            {
                return String.Format("{0:0.##}", Rating);
            }
        }
    }
}