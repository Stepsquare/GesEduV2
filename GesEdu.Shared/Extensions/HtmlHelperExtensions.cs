using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace GesEdu.Shared.Extensions
{
    public static class HtmlHelperExtensions
    {
        //TODO - Implementar na View (https://www.c-sharpcorner.com/article/good-vs-bad-highlight-the-active-page-in-the-nab-bar-for-mvc-razor-view-engine/)
        public static string IsSelected(this IHtmlHelper html, string? controller = null, string? action = null, string? cssClass = null)
        {
            if (string.IsNullOrEmpty(cssClass)) 
                cssClass = "active";
            
            var currentAction = (string)html.ViewContext.RouteData.Values["action"];
            var currentController = (string)html.ViewContext.RouteData.Values["controller"];

            if (string.IsNullOrEmpty(controller)) 
                controller = currentController;

            if (string.IsNullOrEmpty(action)) 
                action = currentAction;

            return controller == currentController && action == currentAction ? cssClass : string.Empty;
        }

        public static bool IsAreaView(this IHtmlHelper html)
        {
            var currentArea = (string)html.ViewContext.RouteData.Values["area"];

            return !string.IsNullOrEmpty(currentArea);
        }

        public static bool IsAreaView(this IHtmlHelper html, string area)
        {
            var currentArea = (string)html.ViewContext.RouteData.Values["area"];

            return currentArea == area;
        }
    }
}
