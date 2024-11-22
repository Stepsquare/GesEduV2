using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text;

namespace GesEdu.Helpers.Breadcrumbs
{
    [HtmlTargetElement("breadcrumb")]
    public class BreadcrumbTagHelper : TagHelper
    {
        private readonly BreadcrumbService _breadcrumbService;

        public BreadcrumbTagHelper(BreadcrumbService breadcrumbService)
        {
            _breadcrumbService = breadcrumbService;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var breadcrumbs = _breadcrumbService.GetBreadcrumbs();

            //Verificar se há breacrumbs...
            if (breadcrumbs == null || breadcrumbs.Count == 0)
            {
                output.SuppressOutput();
                return;
            }

            // Configurar o elemento HTML
            output.TagName = "div";
            output.Attributes.SetAttribute("class", "d-flex");

            // Construir o HTML dos breadcrumbs
            var breadcrumbHtml = new StringBuilder();


            if (breadcrumbs.Count > 1)
            {
                var previousBreadcrumb = breadcrumbs[breadcrumbs.Count - 2];

                breadcrumbHtml.AppendFormat("<a href=\"{0}\" class=\"btn ig-btn-back\" title=\"Voltar\"><i class=\"fas fa-arrow-left\"></i></a>", previousBreadcrumb.Url);
            }

            breadcrumbHtml.Append("<ol class=\"breadcrumb\">");

            for (int i = 0; i < breadcrumbs.Count; i++)
            {
                var breadcrumb = breadcrumbs[i];

                if (i == breadcrumbs.Count - 1) // Último item
                {
                    breadcrumbHtml.AppendFormat("<li class=\"breadcrumb-item active\" aria-current=\"page\">{0}</li>", breadcrumb.Title);
                }
                else // Breadcrumbs com links
                {
                    breadcrumbHtml.AppendFormat("<li class=\"breadcrumb-item\"><a href=\"{0}\">{1}</a></li>", breadcrumb.Url, breadcrumb.Title);
                }
            }

            breadcrumbHtml.Append("</ol>");

            // Define o conteúdo do tag helper
            output.Content.SetHtmlContent(breadcrumbHtml.ToString());
        }
    }
}
