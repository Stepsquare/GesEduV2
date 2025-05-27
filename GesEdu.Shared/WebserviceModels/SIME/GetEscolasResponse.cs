using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GesEdu.Shared.WebserviceModels.SIME
{
    public class GetEscolasResponse
    {
        public List<Escola>? escolas { get; set; }

        public class Escola
        {
            public int cod_escola_me { get; set; }
            public string? nome { get; set; }
            public string? tipologia { get; set; }
            public string? ae_ini { get; set; }
            public string? ae_fim { get; set; }
        }
    }
}
