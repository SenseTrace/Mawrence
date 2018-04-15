using Leng.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Leng.HtmlHelpers
{
    public static class ParagraphHelpers
    {
        public static MvcHtmlString ParagraphTag(this HtmlHelper html, Paragraph paragraph)
        {
            TagBuilder tag;
            switch (paragraph.Category)
            {
                case "code":
                    tag = new TagBuilder("pre");
                    tag.InnerHtml = paragraph.Text;
                    break;
                case "p":
                    tag = new TagBuilder("p");
                    tag.InnerHtml = paragraph.Text;
                    break;
                case "h2":
                    tag = new TagBuilder("h2");
                    tag.InnerHtml = paragraph.Text;
                    break;
                default:
                    tag = new TagBuilder("br");
                    break;
            }
            return MvcHtmlString.Create(tag.ToString());
        }
    }
}