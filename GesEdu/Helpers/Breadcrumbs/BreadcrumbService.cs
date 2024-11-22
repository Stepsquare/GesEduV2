namespace GesEdu.Helpers.Breadcrumbs
{
    public class BreadcrumbService
    {
        private readonly List<BreadcrumbNode> _breadcrumbNodes = new();

        public void AddBreadcrumb(string title, string url)
        {
            //TODO - Falta configurar o comportamento dos breadcrumbs com alterações de area...

            // Verifica se o breadcrumb já existe
            var existingIndex = _breadcrumbNodes.FindIndex(b => b.Url == url);
            if (existingIndex != -1)
            {
                // Remove todos os breadcrumbs que estão à frente na lista
                _breadcrumbNodes.RemoveRange(existingIndex + 1, _breadcrumbNodes.Count - (existingIndex + 1));
            }
            else
            {
                // Se o breadcrumb não existir, adiciona um novo
                _breadcrumbNodes.Add(new BreadcrumbNode(title, url));
            }
        }

        public List<BreadcrumbNode> GetBreadcrumbs()
        {
            return _breadcrumbNodes;
        }
    }
}
