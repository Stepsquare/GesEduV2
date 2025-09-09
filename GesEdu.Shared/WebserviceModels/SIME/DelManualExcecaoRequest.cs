using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GesEdu.Shared.WebserviceModels.SIME
{
    public class DelManualExcecaoRequest
    {
        public string? id_ano_letivo { get; set; }
        public int id_pedido_cab { get; set; }
        public string? utilizador { get; set; }
        public List<Manual> manuais { get; set; } = [];

        public class Manual
        {
            public int id_pedido_man { get; set; }
            public int id_manual { get; set; }
        }
    }
}
