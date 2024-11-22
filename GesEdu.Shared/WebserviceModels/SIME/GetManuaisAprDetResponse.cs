using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GesEdu.Shared.WebserviceModels.SIME
{
    public class GetManuaisAprDetResponse
    {
        public string? id_manual { get; set; }
        public string? id_ano_letivo { get; set; }
        public string? id_ano_escolar { get; set; }
        public string? des_ano_escolar { get; set; }
        public string? titulo { get; set; }
        public string? isbn { get; set; }
        public string? editora { get; set; }
        public decimal preco { get; set; }
        public string? des_disciplina { get; set; }
        public bool certificado { get; set; }
        public bool editavel { get; set; }

        public List<ApreciacaoObj> apreciacoes { get; set; } = new List<ApreciacaoObj>();

        public class ApreciacaoObj
        {
            public int id_valor_dominio { get; set; }
            public string? alinea { get; set; }
            public string? descricao { get; set; }
            public string? avaliacao { get; set; }
            public List<ApreciacaoObj> subalineas { get; set; } = new List<ApreciacaoObj>();
        }
    }
}
