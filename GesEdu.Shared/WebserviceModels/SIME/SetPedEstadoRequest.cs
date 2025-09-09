using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GesEdu.Shared.WebserviceModels.SIME
{
    public class SetPedEstadoRequest
    {
        public int id_pedido_cab { get; set; }
        public string? id_ano_letivo { get; set; }
        public int id_excecao { get; set; }
        public int estado { get; set; }
        public string? justif_estado { get; set; }
        public string? utilizador { get; set; }
    }
}
