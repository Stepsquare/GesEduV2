using GesEdu.Shared.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GesEdu.Shared.SearchParams.SIME
{
    public class GetAlunosManuaisAdaptadosParams : PaginationParams
    {
        public string? TipoDoc { get; set; }
        public string? NumDoc { get; set; }
        public string? Nome { get; set; }
    }
}
