using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GesEdu.Shared.WebserviceModels.SIME
{
    public class DelAnexoPedRequest
    {
        public int id_pedido_cab { get; set; }
        public int id_anexo { get; set; }
        public int tipo_anexo { get; set; }
        public string? utilizador { get; set; }
    }
}
