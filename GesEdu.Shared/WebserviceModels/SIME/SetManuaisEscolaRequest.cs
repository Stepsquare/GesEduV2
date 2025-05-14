using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GesEdu.Shared.WebserviceModels.SIME
{
    public class SetManuaisEscolaRequest
    {
        public string? id_ano_letivo { get; set; }
        public string? cod_uo { get; set; }
        public string? cod_escola_me { get; set; }
        public string? id_manual { get; set; }
        public string? utilizador { get; set; }
        public List<Escola>? escolas { get; set; } = [];

        public class Escola
        {
            public string? cod_escola_me { get; set; }
            public string? num_alunos { get; set; }
        }
    }
}
