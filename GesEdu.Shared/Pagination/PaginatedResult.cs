﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GesEdu.Shared.Pagination
{
    public class PaginatedResult<T> where T : class
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public IEnumerable<T>? Data { get; set; } = Enumerable.Empty<T>();
    }
}
