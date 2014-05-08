using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace UI.Helpers
{
    public static class HtmlExtensions
    {
        public static MvcHtmlString ActionMenuItem(this HtmlHelper htmlHelper, String linkText,
                 String actionName, String controllerName, String additionalСlasses = null)
        {

            var tag = new TagBuilder("li");
            
            if (htmlHelper.ViewContext.RequestContext.IsCurrentRoute(null, controllerName, actionName))
            {
                tag.AddCssClass("selected");
            }

            if (!string.IsNullOrEmpty(additionalСlasses))
            {
                tag.AddCssClass(additionalСlasses);
            }

            tag.InnerHtml = htmlHelper.ActionLink(linkText, actionName, controllerName).ToString();

            return MvcHtmlString.Create(tag.ToString());
        }
    }
}