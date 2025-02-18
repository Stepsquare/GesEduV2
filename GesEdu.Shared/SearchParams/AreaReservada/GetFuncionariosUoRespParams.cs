using GesEdu.Shared.Pagination;

namespace GesEdu.Shared.SearchParams.AreaReservada
{
    public class GetFuncionariosUoRespParams : PaginationParams
    {
        public string? nif_pes { get; set; }
        public string? regime { get; set; }
    }
}
