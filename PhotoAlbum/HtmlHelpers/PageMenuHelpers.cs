using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace PhotoAlbum.HtmlHelpers
{
    public static class PageMenuHelpers
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html, int numberPage, int countPage, Func<int, string> url)
        {
            StringBuilder builder = new StringBuilder();
            
            for(int i = 1; i <= countPage; i++)
            {
                TagBuilder a = new TagBuilder("a");
                a.InnerHtml = i.ToString();

                if (i == numberPage)
                {
                    a.MergeAttribute("href", "#");
                    builder.AppendLine("<li style = \"background-color: orange; \">" + a.ToString() + "</li>");
                }
                else
                {
                    a.MergeAttribute("href", url(i));
                    builder.AppendLine("<li>" + a.ToString() + "</li>");
                }
            }

            return new MvcHtmlString("<ul class=\"navigate\"> " + builder.ToString() + "</ul>");
        }
    }
}