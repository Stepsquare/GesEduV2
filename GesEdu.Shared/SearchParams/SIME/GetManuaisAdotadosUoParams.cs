using GesEdu.Shared.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GesEdu.Shared.SearchParams.SIME
{
    public class GetManuaisAdotadosUoParams : PaginationParams
    {
        public string? id_ano_letivo { get; set; }
        public string? cod_escola_me { get; set; }
        public string? id_ano_escolar { get; set; }
        public string? id_disciplina { get; set; }
        public string? filtro { get; set; }
        public string? estado_estimativa { get; set; }
        public string? tipologia { get; set; }
    }
}
