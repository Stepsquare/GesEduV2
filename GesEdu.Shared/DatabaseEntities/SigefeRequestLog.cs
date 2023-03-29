using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GesEdu.Shared.DatabaseEntities
{
    public class SigefeRequestLog
    {
        public int Id { get; set; }
        public string? CodUo { get; set; }
        public string? User { get; set; }
        public string? WebServiceUrl { get; set; }
        public string? WebServiceHttpMethod { get; set; }
        public string? RequestHeaders { get; set; }
        public string? JsonRequest { get; set; }
        public string? JsonResponse { get; set; }
        public string? HttpStatusCode { get; set; }
        public string? HttpStatusMessage { get; set; }
        public DateTime RequestDate { get; set; }
    }
}
