using Microsoft.AspNetCore.Mvc.Rendering;

namespace GesEdu.Helpers
{
    public static class HtmlExtensions
    {
        public static string IsActive(this IHtmlHelper html, string[] actions, string[] controllers, string? area = null, string? cssClass = null)
        {
            if (string.IsNullOrEmpty(cssClass))
                cssClass = "active";

            string? currentAction = (string?)html.ViewContext.RouteData.Values["action"];
            string? currentController = (string?)html.ViewContext.RouteData.Values["controller"];
            string? currentArea = (string?)html.ViewContext.RouteData.Values["area"];

            if (string.IsNullOrEmpty(area))
                area = currentArea;

            return area == currentArea && controllers.Contains(currentController) && actions.Contains(currentAction) ? cssClass : string.Empty;
        }

        public static string IsCollapsed(this IHtmlHelper html, string[] actions, string[] controllers, string? area = null, string? cssClass = null)
        {
            if (string.IsNullOrEmpty(cssClass))
                cssClass = "active";

            string? currentAction = (string?)html.ViewContext.RouteData.Values["action"];
            string? currentController = (string?)html.ViewContext.RouteData.Values["controller"];
            string? currentArea = (string?)html.ViewContext.RouteData.Values["area"];

            if (string.IsNullOrEmpty(area))
                area = currentArea;

            return area == currentArea && controllers.Contains(currentController) && actions.Contains(currentAction) ? string.Empty : cssClass;
        }

        public static bool IsAreaView(this IHtmlHelper html, string? area = null)
        {
            string? currentArea = (string?)html.ViewContext.RouteData.Values["area"];

            return !string.IsNullOrEmpty(area) ? currentArea == area : !string.IsNullOrEmpty(currentArea);
        }
    }
}
