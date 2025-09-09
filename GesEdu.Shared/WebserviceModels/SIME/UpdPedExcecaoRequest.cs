using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GesEdu.Shared.WebserviceModels.SIME
{
    public class UpdPedExcecaoRequest
    {
        public int id_pedido_cab { get; set; }
        public string? cod_uo { get; set; }
        public string? cod_escola_me { get; set; }
        public string? id_ano_letivo { get; set; }
        public int id_excecao { get; set; }
        public string? utilizador { get; set; }
        public string? justif_pedido { get; set; }

        public List<Manual> manuais { get; set; } = new List<Manual>();

        public class Manual
        {
            public int id_manual { get; set; }

            public List<Escola> escolas { get; set; } = [];

            public class Escola
            {
                public int cod_escola_me { get; set; }
                public int num_alunos { get; set; }
            }
        }
    }
}
