namespace GesEdu.Helpers.Breadcrumbs
{
    public class BreadcrumbNode
    {
        public BreadcrumbNode(string title, string url)
        {
            Title = title;
            Url = url;
        }

        public string? Title { get; set; }
        public string? Url { get; set; }
    }
}
