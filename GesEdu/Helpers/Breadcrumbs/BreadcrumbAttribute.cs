using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc;

namespace GesEdu.Helpers.Breadcrumbs
{
    public class BreadcrumbAttribute : ActionFilterAttribute, IActionFilter
    {
        public string? Title { get; set; }
        public bool ResetAreaNodes { get; set; } = true;

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            // Injectar o serviço de breadcumbs...
            var breadcrumbService = context.HttpContext.RequestServices.GetService<BreadcrumbService>();

            // Verificar acessibilidade ao breadcrumb service...
            if (breadcrumbService == null)
            {
                base.OnActionExecuted(context);
                return;
            }

            #region Breadcrumb/Page Title

            var controller = context.Controller as Controller;

            // Verificar se o titulo foi passado como parametro ou se tem que ser obtido da ViewData["BreadcrumbTitle"]...
            if (string.IsNullOrEmpty(Title))
                if (controller?.ViewData["BreadcrumbTitle"] is string viewDataTitle)
                    Title = viewDataTitle;

            #endregion

            // Obter a area, controller e action...
            string? areaValue = context.RouteData.Values["Area"]?.ToString();
            string? controllerValue = context.RouteData.Values["Controller"]?.ToString();
            string? actionValue = context.RouteData.Values["Action"]?.ToString();

            // Configura parâmetros adicionais para o URL a partir dos parâmetros da ação...
            var routeValues = new RouteValueDictionary(context.HttpContext.Items["ActionArguments"] as IDictionary<string, object>);

            // Adicionar o parametro referente à Area se existir
            if (!string.IsNullOrEmpty(areaValue))
                routeValues["Area"] = areaValue;

            // Gerar url recorrendo ao urlHelper
            var urlHelper = context.HttpContext.RequestServices.GetService<IUrlHelperFactory>()?.GetUrlHelper(context);
            var url = urlHelper?.Action(actionValue, controllerValue, routeValues);

            breadcrumbService!.AddBreadcrumb(Title!, url!, areaValue, ResetAreaNodes);

            base.OnActionExecuted(context);
        }
    }
}
