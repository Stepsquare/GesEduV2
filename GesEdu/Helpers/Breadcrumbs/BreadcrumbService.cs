namespace GesEdu.Helpers.Breadcrumbs
{
    public class BreadcrumbService
    {
        private readonly List<BreadcrumbNode> _breadcrumbNodes = new();
        private string? _currentArea;

        public void AddBreadcrumb(string title, string url, string? newNodeArea, bool resetAreaNodes)
        {
            var existingIndex = _breadcrumbNodes.FindIndex(b => b.Url == url);

            // Verifica se o breadcrumb já existe
            if (existingIndex != -1)
            {
                // Se o breadcrumb existir remove todos os breadcrumbs que estão à frente na lista...
                _breadcrumbNodes.RemoveRange(existingIndex + 1, _breadcrumbNodes.Count - (existingIndex + 1));
            }
            else
            {
                // Verifica se nos encontramos numa Area
                if (!string.IsNullOrEmpty(_currentArea))
                {
                    // Verifica se o node a inserir é na mesma Area que o anterior
                    if (!_currentArea.Equals(newNodeArea))
                    {
                        // Remove todos os node à excepção do primeiro referente à Homepage GesEDU...
                        _breadcrumbNodes.RemoveRange(1, _breadcrumbNodes.Count - 1);
                    }
                    else
                    {
                        if (resetAreaNodes && _breadcrumbNodes.Count > 2)
                        {
                            // Remove todos os nodes até à Home da Area
                            _breadcrumbNodes.RemoveRange(2, _breadcrumbNodes.Count - 2);
                        }
                    }
                }

                // Adiciona o novo BreadcrumbNode...
                _breadcrumbNodes.Add(new BreadcrumbNode(title, url));

                // Atualiza a propriedade _currentArea com a nova Area
                _currentArea = newNodeArea;
            }
        }

        public List<BreadcrumbNode> GetBreadcrumbs()
        {
            return _breadcrumbNodes;
        }
    }
}
