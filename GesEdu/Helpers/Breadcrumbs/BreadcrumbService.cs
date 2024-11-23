namespace GesEdu.Helpers.Breadcrumbs
{
    public class BreadcrumbService
    {
        private readonly List<BreadcrumbNode> _breadcrumbNodes = new();
        private string? _currentArea;

        public void AddBreadcrumb(string title, string url, string? newNodeArea)
        {
            // Verifica se o breadcrumb já existe
            var existingIndex = _breadcrumbNodes.FindIndex(b => b.Url == url);
            if (existingIndex != -1)
            {
                // Se o breadcrumb existir remove todos os breadcrumbs que estão à frente na lista...
                _breadcrumbNodes.RemoveRange(existingIndex + 1, _breadcrumbNodes.Count - (existingIndex + 1));
            }
            else
            {
                // Se o breadcrumb não existir primeiro verifica se é página da mesma Area
                if (!string.IsNullOrEmpty(_currentArea) && !_currentArea.Equals(newNodeArea))
                {
                    //Remove todos os node à excepção do primeiro referente à Homepage GesEDU...
                    _breadcrumbNodes.RemoveRange(1, _breadcrumbNodes.Count - 1);
                }

                //Adiciona o novo BreadcrumbNode...
                _breadcrumbNodes.Add(new BreadcrumbNode(title, url));

                //Atualiza a propriedade _currentArea com a nova Area
                _currentArea = newNodeArea;
            }
        }

        public List<BreadcrumbNode> GetBreadcrumbs()
        {
            return _breadcrumbNodes;
        }
    }
}
