using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GesEdu.Shared.WebserviceModels.Login
{
    public class AlterarPasswordRequest
    {
        public string? username { get; set; }
        public string? current_password { get; set; }
        public string? new_password { get; set; }
    }
}
