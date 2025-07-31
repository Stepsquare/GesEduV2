using GesEdu.Shared.Pagination;

namespace GesEdu.Shared.SearchParams.SIME
{
    public class GetPedidosExcecaoParams : PaginationParams
    {
        public string? ano_letivo { get; set; }
        public string? ano_escolar { get; set; }
        public string? id_disciplina { get; set; }
        public string? tipo_excecao { get; set; }
        public string? estado_excecao { get; set; }
    }
}
