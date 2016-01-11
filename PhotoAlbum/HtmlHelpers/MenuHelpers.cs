using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace PhotoAlbum.HtmlHelpers
{
    public static class MenuHelpers
    {
        static Dictionary<string, string> menu = new Dictionary<string, string>()
        {
            {"Домой", "/Home/Index"},
            {"Альбомы", "/Album/ShowAlbums"},
            {"Управление польователями", "/Admin/ShowUsers"}
        };

        public static MvcHtmlString MenuShowSelected(this HtmlHelper html, string menuSelected, bool isAdmin)
        {
            StringBuilder result = new StringBuilder();

            foreach (var item in menu)
            {
                if (!isAdmin && item.Key == "Управление польователями")
                    continue;

                TagBuilder li = new TagBuilder("li");
           
                TagBuilder a = new TagBuilder("a");
                a.MergeAttribute("href", item.Value);
                if (item.Key == menuSelected)
                    a.AddCssClass("selected");
                a.InnerHtml = item.Key;   

                li.InnerHtml = a.ToString();
                result.Append(li.ToString());
            }

            return MvcHtmlString.Create(result.ToString());
        } 


    }
}