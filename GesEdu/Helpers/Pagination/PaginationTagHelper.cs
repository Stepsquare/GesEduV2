using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text;

namespace GesEdu.Helpers.Pagination
{
    [HtmlTargetElement("pagination")]
    public class PaginationTagHelper : TagHelper
    {
        private const int _maxNumberOfWidgetPages = 5;

        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get { return (int)Math.Ceiling(TotalCount / (decimal)PageSize); } }
        public string AjaxRequestMethod { get; set; } = string.Empty;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Attributes.SetAttribute("class", "row ig-pagination");

            var content = new StringBuilder();

            int firstResult = (PageSize * (PageIndex - 1)) + 1;
            int lastResult = Math.Min(PageSize * PageIndex, TotalCount);

            // Parte da esquerda (info)
            content.AppendLine(@"<div class='col-md align-middle'>");
            content.AppendLine($"<p class='ig-grid-record'>{firstResult} a {lastResult} de {TotalCount} registos</p>");
            content.AppendLine("</div>");

            // Parte da direita (paginação)
            content.AppendLine("<div class='col-md-auto'>");
            content.AppendLine("<nav aria-label='Page navigation example'><ul class='pagination mb-0 float-end'>");

            // Primeira página
            content.AppendLine(RenderPageItem(1, "Início", "fas fa-angles-left", PageIndex == 1));

            // Página anterior
            content.AppendLine(RenderPageItem(PageIndex - 1, "Anterior", "fas fa-angle-left", PageIndex == 1));

            // Cálculo de páginas visíveis
            int firstPageOnDisplay = TotalPages >= _maxNumberOfWidgetPages && PageIndex + (_maxNumberOfWidgetPages / 2) > TotalPages
                                        ? TotalPages - _maxNumberOfWidgetPages + 1
                                        : Math.Max(1, PageIndex - (_maxNumberOfWidgetPages / 2));

            int lastPageOnDisplay = Math.Min(TotalPages, PageIndex + (_maxNumberOfWidgetPages / 2) + Math.Max(0, firstPageOnDisplay - PageIndex + (_maxNumberOfWidgetPages / 2)));

            for (int i = firstPageOnDisplay; i <= lastPageOnDisplay; i++)
            {
                string activeClass = PageIndex == i ? "active" : "";
                content.AppendLine($@"
                    <li class='page-item {activeClass}' onclick='{string.Format(AjaxRequestMethod, i, PageSize)}'>
                        <a class='page-link' href='javascript:void(0);'>{i}</a>
                    </li>"
                );
            }

            // Próxima página
            content.AppendLine(RenderPageItem(PageIndex + 1, "Seguinte", "fas fa-angle-right", PageIndex == TotalPages));

            // Última página
            content.AppendLine(RenderPageItem(TotalPages, "Fim", "fas fa-angles-right", PageIndex == TotalPages));

            content.AppendLine("</ul></nav></div>");

            output.Content.SetHtmlContent(content.ToString());
        }

        private string RenderPageItem(int page, string title, string iconClass, bool disabled)
        {
            var disabledClass = disabled ? "disabled" : "";
            var onclick = disabled ? "" : $"onclick='{string.Format(AjaxRequestMethod, page, PageSize)}'";
            return $@"
                <li class='page-item {disabledClass}' {onclick}>
                    <a class='page-link' href='javascript:void(0);' title='{title}'><i class='{iconClass}'></i></a>
                </li>";
        }
    }
}
