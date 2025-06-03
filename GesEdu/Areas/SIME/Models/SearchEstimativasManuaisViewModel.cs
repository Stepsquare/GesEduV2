using GesEdu.Shared.Pagination;
using GesEdu.Shared.WebserviceModels.SIME;

namespace GesEdu.Areas.SIME.Models
{
    public class SearchEstimativasManuaisViewModel
    {
        public bool IsReadOnly { get; set; }
        public PaginatedResult<GetManuaisAdotadosUoResponse.Manual>? Results { get; set; }
    }
}
