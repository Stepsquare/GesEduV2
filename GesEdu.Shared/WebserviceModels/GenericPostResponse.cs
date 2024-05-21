using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GesEdu.Shared.WebserviceModels
{
    public class GenericPostResponse
    {
        public int ws_id { get; set; }
        public string? ws_name { get; set; }
        public string? ws_version { get; set; }
        public string? ws_date_stamp { get; set; }
        public List<Message> messages { get; set; } = new List<Message>();

        public class Message
        {
            public string? cod_msg { get; set; }
            public string? msg { get; set; }
        }
    }
}
