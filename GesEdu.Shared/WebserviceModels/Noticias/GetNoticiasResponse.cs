using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GesEdu.Shared.WebserviceModels.Noticias
{
    public class GetNoticiasResponse
    {
        public List<GetNoticiasObj> noticias { get; set; } = new List<GetNoticiasObj>();

        public class GetNoticiasObj
        {
            public int id_noticia { get; set; }
            public string? titulo { get; set; }
            public string? resumo { get; set; }
            public string? corpo { get; set; }
            public string? dt_publicacao { get; set; }
        }
    }
}
