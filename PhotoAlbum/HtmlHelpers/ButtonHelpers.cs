using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace PhotoAlbum.HtmlHelpers
{
    public static class ButtonHelpers
    {
        public static MvcHtmlString ButtonsRatingSelected(this HtmlHelper html, int value)
        {
            StringBuilder result = new StringBuilder();

            for(int i = 1; i <= 5; i++)
            {
                if(i == value)
                    result.Append($"<button class=\"selected\" name=\"rating\" value={i}>{i}</button>");
                else
                    result.Append($"<button name=\"rating\" value={i}>{i}</button>");
            }

            return MvcHtmlString.Create(result.ToString());
        }
    }
}