using GesEdu.Shared.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GesEdu.Shared.SearchParams
{
    public class GetUtilizadoresParams : PaginationParams
    {
        public string? Username { get; set; }
        public bool? IsActive { get; set; }
        public bool IsIgefeUsers { get; set; } = false;
    }
}
