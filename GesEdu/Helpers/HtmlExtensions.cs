using Microsoft.AspNetCore.Mvc.Rendering;

namespace GesEdu.Helpers
{
    public static class HtmlExtensions
    {
        public static string IsSelected(this IHtmlHelper html, string? area = null, string? controller = null, string? action = null, string? cssClass = null)
        {
            if (string.IsNullOrEmpty(cssClass))
                cssClass = "active";

            string? currentAction = (string?)html.ViewContext.RouteData.Values["action"];
            string? currentController = (string?)html.ViewContext.RouteData.Values["controller"];
            string? currentArea = (string?)html.ViewContext.RouteData.Values["area"];

            if (string.IsNullOrEmpty(controller))
                area = currentArea;

            if (string.IsNullOrEmpty(controller))
                controller = currentController;

            if (string.IsNullOrEmpty(action))
                action = currentAction;

            return area == currentArea && controller == currentController && action == currentAction ? cssClass : string.Empty;
        }

        public static bool IsAreaView(this IHtmlHelper html)
        {
            string? currentArea = (string?)html.ViewContext.RouteData.Values["area"];

            return !string.IsNullOrEmpty(currentArea);
        }

        public static bool IsAreaView(this IHtmlHelper html, string area)
        {
            string? currentArea = (string?)html.ViewContext.RouteData.Values["area"];

            return currentArea == area;
        }
    }
}
