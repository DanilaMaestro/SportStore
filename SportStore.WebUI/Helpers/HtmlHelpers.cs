using SportStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace SportStore.WebUI.Helpers
{
    public static class HtmlHelpers
    {
        public static MvcHtmlString HtmlLinks(this HtmlHelper helper, 
            PagingInfo info, Func<int, string> urlDelegat )
        {
            StringBuilder txtBuild = new StringBuilder();

            for (int i = 1; i <= info.PageCount; i++)
            {
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href", urlDelegat(i));
                tag.InnerHtml = i.ToString();

                if (i == info.CurrentPage) { tag.MergeAttribute("class", "selected"); }

                txtBuild.Append(tag.ToString());
            }

            return MvcHtmlString.Create(txtBuild.ToString());
        }

    }
}