using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GesEdu.Shared.DatabaseEntities
{
    public class WebServiceError
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string? User { get; set; }
        public string? Url { get; set; }
        public string? HttpMethod { get; set; }
        public string? RequestHeaders { get; set; }
        public string? RequestContent { get; set; }
        public string? ResponseContent { get; set; }
        public string? ResponseContentType { get; set; }
        public int StatusCode { get; set; }
        public string? StatusMessage { get; set; }
        public DateTime RequestDate { get; set; } = DateTime.Now;
    }
}
