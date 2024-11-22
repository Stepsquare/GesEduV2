using GesEdu.Shared.Pagination;

namespace GesEdu.Shared.SearchParams.SIME
{
    public class GetManuaisApreciadosParams : PaginationParams
    {
        public string? custom { get; set; }
        public string? ano_letivo { get; set; }
        public string? ano_escolar { get; set; }
        public string? id_disciplina { get; set; }
        public string? estado { get; set; }
        public string? certificado { get; set; }
        public string? tipologia { get; set; }
    }
}
